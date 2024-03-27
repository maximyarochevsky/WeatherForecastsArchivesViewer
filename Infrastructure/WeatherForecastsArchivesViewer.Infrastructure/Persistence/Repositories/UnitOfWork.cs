using WeatherForecastsArchivesViewer.Application.Interfaces.Persistence;
using WeatherForecastsArchivesViewer.Application.Interfaces.Persistence.Repositories;

namespace WeatherForecastsArchivesViewer.Infrastructure.Persistence.Repositories;

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
