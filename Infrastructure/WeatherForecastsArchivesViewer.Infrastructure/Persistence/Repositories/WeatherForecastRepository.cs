using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherForecastsArchivesViewer.Application.Interfaces.Persistence.Repositories;
using WeatherForecastsArchivesViewer.Domain.Entities;

namespace WeatherForecastsArchivesViewer.Infrastructure.Persistence.Repositories;

/// <inheritdoc cref="IWeatherForecastRepository"/>
public class WeatherForecastRepository : IWeatherForecastRepository
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    private readonly DatabaseContext _dbContext;

    /// <summary>
    /// Логгер для ведения журнала работы программы.
    /// </summary>
    private readonly ILogger<WeatherForecastRepository> _logger;

    public WeatherForecastRepository(
        DatabaseContext dbContext,
        ILogger<WeatherForecastRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    /// <inheritdoc cref="IWeatherForecastRepository.AddRangeForecasts(List{WeatherForecastEntity})"/>
    public async Task AddRangeForecasts(List<WeatherForecastEntity> forecasts, CancellationToken cancellationToken)
    {
        WeatherForecastEntity df = new WeatherForecastEntity();
        try
        {
            await _dbContext.AddRangeAsync(forecasts, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when adding new data to the database.");
            throw;
        }
    }

    /// <inheritdoc cref="IWeatherForecastRepository.GetForecastsByYear(int)"/>
    public async Task<List<WeatherForecastEntity>> GetForecastsByYear(int year, CancellationToken cancellationToken)
    {
        try
        {
            return await _dbContext.WeatherForecasts.Where(forecast => forecast.Date.Year == year).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when searching for forecasts by year.");
            throw;
        }
    }

    /// <inheritdoc cref="IWeatherForecastRepository.GetAllForecasts"/>
    public async Task<List<WeatherForecastEntity>> GetAllForecasts(CancellationToken cancellationToken)
    {
        try
        {
            return await _dbContext.WeatherForecasts.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when requesting all data from the database.");
            throw;
        }
    }
}
