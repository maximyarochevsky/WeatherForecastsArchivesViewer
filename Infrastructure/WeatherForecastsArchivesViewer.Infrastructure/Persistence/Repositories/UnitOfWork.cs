using WeatherForecastsArchiveViewer.Application.Interfaces.Persistence;
using WeatherForecastsArchiveViewer.Application.Interfaces.Persistence.Repositories;

namespace WeatherForecastsArchiveViewer.Infrastructure.Persistence.Repositories;

/// <inheritdoc cref="IUnitOfWork"/>
public class UnitOfWork : IUnitOfWork
{ 
    /// <inheritdoc cref="IWeatherForecastRepository"/>
    public IWeatherForecastRepository WeatherForecast { get; set; }

    public UnitOfWork(IWeatherForecastRepository hourlyWeatherForecast)
    {
            WeatherForecast = hourlyWeatherForecast;
    }
}
