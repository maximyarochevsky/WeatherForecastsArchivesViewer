using WeatherForecastsArchivesViewer.Application.Interfaces.Persistence.Repositories;

namespace WeatherForecastsArchivesViewer.Application.Interfaces.Persistence;

/// <summary>
/// Провайдер репозиториев.
/// </summary>
public interface IUnitOfWork
{
    /// <inheritdoc cref="IWeatherForecastRepository"/>
    IWeatherForecastRepository WeatherForecast { get; set; }
}
