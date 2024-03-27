using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeatherForecastsArchivesViewer.Application.Interfaces.Persistence;
using WeatherForecastsArchivesViewer.Application.Interfaces.Services;
using WeatherForecastsArchivesViewer.Application.WeatherForecast.DTO;
using WeatherForecastsArchivesViewer.Domain.Entities;

namespace WeatherForecastsArchivesViewer.Application.WeatherForecast.Commands.UploadWeatherArchiveCommand;

/// <summary>
/// Обработчик запроса на загрузку архива(-ов) прогнозов погоды.
/// </summary>
public class UploadWeatherArchiveHandler : IRequestHandler<UploadWeatherArchive, bool>
{
    /// <summary>
    /// Логгер для ведения журнала работы программы.
    /// </summary>
    private readonly ILogger<UploadWeatherArchiveHandler> _logger;

    /// <summary>
    /// Парсер excel файлов с прогнозами.
    /// </summary>
    private readonly IExcelWeatherArchiveReader _excelReader;

    /// <summary>
    /// Провайдер сервисов.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Маппер для преобразовния объектов.
    /// </summary>
    private readonly IMapper _mapper;

    public UploadWeatherArchiveHandler(
        ILogger<UploadWeatherArchiveHandler> logger,
        IExcelWeatherArchiveReader excelReader,
        IServiceProvider serviceProvider,
        IMapper mapper
        )
    {
        _logger = logger;
        _excelReader = excelReader;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    /// <inheritdoc cref="IRequestHandler{TRequest, TResponse}.Handle(TRequest, CancellationToken)"/>
    public async Task<bool> Handle(UploadWeatherArchive request, CancellationToken cancellationToken)
    {
        try
        {
            var files = request.files;

            using var scope = _serviceProvider.CreateScope();
            var databaseProvider = scope.ServiceProvider.GetRequiredService<IDatabasePort>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileYear = _excelReader.GetFileYear(file);

                    var dbForecasts = await databaseProvider.Execute(async u => await u.WeatherForecast.GetForecastsByYear(fileYear, cancellationToken));
                    var dbForecastsDto = _mapper.Map<List<WeatherForecastDto>>(dbForecasts);

                    var forecastArchive = _excelReader.ReadExcelFile(file, fileYear);

                    var newForecasts = forecastArchive.Except(dbForecastsDto).ToList();
                    var newForecastsEntities = _mapper.Map<List<WeatherForecastEntity>>(newForecasts);

                    databaseProvider.Execute(u => u.WeatherForecast.AddRangeForecasts(newForecastsEntities));
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing the archive download request");
            throw;
        }

        return true;
    }
}
