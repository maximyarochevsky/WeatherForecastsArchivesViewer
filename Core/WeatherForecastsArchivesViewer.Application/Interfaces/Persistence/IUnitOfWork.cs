using WeatherForecastsArchiveViewer.Application.Interfaces.Persistence.Repositories;

namespace WeatherForecastsArchiveViewer.Application.Interfaces.Persistence;

/// <summary>
/// Провайдер репозиториев.
/// </summary>
public interface IUnitOfWork
{
    /// <inheritdoc cref="IWeatherForecastRepository"/>
    IWeatherForecastRepository WeatherForecast { get; set; }
}
