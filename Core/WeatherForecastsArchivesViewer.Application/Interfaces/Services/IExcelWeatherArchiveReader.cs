using Microsoft.AspNetCore.Http;
using WeatherForecastsArchivesViewer.Application.WeatherForecast.DTO;

namespace WeatherForecastsArchivesViewer.Application.Interfaces.Services;

/// <summary>
/// Класс для чтения и парсинга excel файлов.
/// </summary>
public interface IExcelWeatherArchiveReader
{
    /// <summary>
    /// Чтение excel архива прогнозов в список.
    /// </summary>
    List<WeatherForecastDto>? ReadExcelFile(IFormFile file, int fileYear);

    /// <summary>
    /// Вовзращает год, по которому содержатся данные прогнозов в файле.
    /// </summary>
    int GetFileYear(IFormFile file);
}
