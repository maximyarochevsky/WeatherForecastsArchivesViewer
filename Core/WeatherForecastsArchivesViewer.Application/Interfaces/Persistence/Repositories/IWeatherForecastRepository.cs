using WeatherForecastsArchivesViewer.Domain.Entities;

namespace WeatherForecastsArchivesViewer.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Хранилище прогнозов.
/// </summary>
public interface IWeatherForecastRepository
{
    /// <summary>
    /// Добавление списка новых прогнозов в БД. 
    /// </summary>
    Task AddRangeForecasts(List<WeatherForecastEntity> forecasts, CancellationToken cancellationToken);

    /// <summary>
    /// Получение прогнозов за нужный год.
    /// </summary>
    Task<List<WeatherForecastEntity>> GetForecastsByYear(int year, CancellationToken cancellationToken);

    /// <summary>
    /// Получение всех данных по прогнозам из БД.
    /// </summary>
    Task<List<WeatherForecastEntity>> GetAllForecasts(CancellationToken cancellationToken);
}
