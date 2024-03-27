using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Text.RegularExpressions;
using WeatherForecastsArchivesViewer.Application.Common.Exceptions;
using WeatherForecastsArchivesViewer.Application.WeatherForecast.DTO;
using WeatherForecastsArchivesViewer.Application.Interfaces.Services;
using WeatherForecastsArchivesViewer.Domain.Enums;

namespace WeatherForecastsArchivesViewer.Infrastructure.Services;

/// <inheritdoc cref="IExcelWeatherArchiveReader"/>
public class ExcelWeatherArchiveReader : IExcelWeatherArchiveReader
{
    /// <summary>
    /// Логгер для ведения журнала работы программы.
    /// </summary>
    private readonly ILogger<ExcelWeatherArchiveReader> _logger;

    /// <summary>
    /// Направления ветра.
    /// </summary>
    private readonly Dictionary<string, WindDirection?> _windDirections = new Dictionary<string, WindDirection?>
    {
        {" ", null},
        {"С", WindDirection.North},
        {"Ю", WindDirection.South},
        {"З", WindDirection.West},
        {"В", WindDirection.East},
        {"СЗ", WindDirection.Northwest},
        {"СВ", WindDirection.Northeast},
        {"ЮЗ", WindDirection.Southwest},
        {"ЮВ", WindDirection.Southeast},
        {"С,СЗ", WindDirection.NorthNorthwest},
        {"С,СВ", WindDirection.NorthNortheast},
        {"Ю,ЮЗ", WindDirection.SouthSouthwest},
        {"Ю,ЮВ", WindDirection.SouthSoutheast},
        {"З,ЮЗ", WindDirection.WestSouthwest},
        {"В,ЮВ", WindDirection.EastSoutheast},
        {"З,СЗ", WindDirection.WestNorthwest},
        {"В,СВ", WindDirection.EastNortheast},
        {"штиль", WindDirection.Calm}
    };

    public ExcelWeatherArchiveReader(ILogger<ExcelWeatherArchiveReader> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc cref="IExcelWeatherArchiveReader.ReadExcelFile(IFormFile, int)"/>
    public List<WeatherForecastDto>? ReadExcelFile(IFormFile file, int fileYear)
    {
        try
        {
            XSSFWorkbook excelList;
            using (var stream = file.OpenReadStream())
            {
                excelList = new XSSFWorkbook(stream);
            }

            var forecastArchive = ReadExcelListWithMounthToListDto(excelList, fileYear);

            return forecastArchive;
        }
        catch (InvalidFileNameException ex)
        {
            _logger.LogError(ex, "Error processing file: {fileName}", file.FileName);
            throw; 
        }
        catch (Exception ex)
        {
            var errorMessage = $"Ошибка при обработке файла {file.FileName}.";
            _logger.LogError(ex, errorMessage);
            throw new ExcelProcessingException(errorMessage, ex);
        }

    }

    /// <inheritdoc cref="IExcelWeatherArchiveReader.GetFileYear"/>
    public int GetFileYear(IFormFile file)
    {
        if (IsCorrectFileNameAndFormat(file))
        {
            // Берет только год из названия.
            return int.Parse(file.FileName.Split('.')[0][^4..]);
        }
        else
        {
            var errorMessage = $"Имя файла {file.FileName} не соответствует шаблону \"moskva_год\" или формату \".xlsx\".";
            _logger.LogError(errorMessage);
            throw new InvalidFileNameException(errorMessage);
        }
    }
    
    /// <summary>
    /// Првоерка имени файла.
    /// </summary>
    public bool IsCorrectFileNameAndFormat(IFormFile file)
    {
        var match = Regex.Match(file.FileName, @"moskva_(\d{4})");

        return match.Success &&
            (file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" ||
             file.ContentType == "application/vnd.ms-excel");
    }

    /// <summary>
    /// Чтение excel листа с прогнозами на месяц в список.
    /// </summary>
    private List<WeatherForecastDto> ReadExcelListWithMounthToListDto(XSSFWorkbook excelList, int fileYear)
    {
      
        var mounthForecasts = new List<WeatherForecastDto>();

        for (var i = 0; i < 12; i++)
        {
            var sheet = excelList.GetSheetAt(i);

            for (var rowIdx = 5; rowIdx <= sheet.LastRowNum; rowIdx++)
            {
                var row = sheet.GetRow(rowIdx);

                if (row == null)
                {
                    continue;
                }

                var forecast = ReadRowToDto(row, fileYear);

                mounthForecasts.Add(forecast);
            }
        }

        return mounthForecasts;
    }

    /// <summary>
    /// Читает строку с часовым прогнозом погоды в экземпляр класса.
    /// </summary>
    private WeatherForecastDto? ReadRowToDto(IRow row, int fileYear)
    {
        try
        {
            var yearRow = DateOnly.Parse(row.GetCell(0).StringCellValue.ToString());

            if (yearRow.Year != fileYear)
            {
                throw new InvalidRowDataException($"Год ячейки даты в строке {row.RowNum + 1} листа {row.Sheet.SheetName} совпадает с годом прогноза.");
            }

            var forecast = new WeatherForecastDto
                (
                    yearRow,
                    TimeOnly.Parse(row.GetCell(1).StringCellValue.ToString()),
                    (float)row.GetCell(2).NumericCellValue,
                    (float)row.GetCell(3).NumericCellValue,
                    (float)row.GetCell(4).NumericCellValue,
                    (int)row.GetCell(5).NumericCellValue,
                    GetWindDirectionByCellValue(row.GetCell(6)),
                    TryGetIntValue(row.GetCell(7)),
                    TryGetIntValue(row.GetCell(8)),
                    TryGetIntValue(row.GetCell(9)),
                    TryGetIntValue(row.GetCell(10)),
                    row.GetCell(11)?.StringCellValue
                );

            return forecast;
        }
        catch (InvalidRowDataException ex)
        {
            _logger.LogError(ex, "The year of the cell does not match the year of the document.");
            throw;
        }
        catch (Exception ex)
        {
            var errorMessage = $"Invalid data or missing required data in the {row.RowNum + 1} row of the {row.Sheet.SheetName} sheet";
            _logger.LogError(ex, errorMessage);
            throw new InvalidRowDataException(
                $"Некорректные данные или отсутвуют обязательные данные в строке {row.RowNum + 1} листа {row.Sheet.SheetName}");
        }
    }

    /// <summary>
    /// Отдает сопоставление перечисления направления ветра.
    /// </summary>
    private WindDirection? GetWindDirectionByCellValue(ICell cell)
    {
        var cellValue = cell.StringCellValue;
        if (_windDirections.TryGetValue(cellValue, out var value))
        {
            return value;
        }
        else
        {
            throw new InvalidRowDataException($"Некорректное значение ячейки в \"Направление ветра\" в строке {cell.Row.RowNum + 1} листа {cell.Sheet.SheetName}");
        }
    }

    /// <summary>
    /// Возвращает значение ячейки или null.
    /// </summary>
    private static int? TryGetIntValue(ICell cell)
    {
        if (cell == null)
        {
            throw new InvalidRowDataException($"Некорректное значение ячейки численного типа.");
        }
        if (cell.CellType == CellType.Numeric)
        {
            return (int?)cell.NumericCellValue;
        }
        else
        {
            return null;
        }
    }
}
