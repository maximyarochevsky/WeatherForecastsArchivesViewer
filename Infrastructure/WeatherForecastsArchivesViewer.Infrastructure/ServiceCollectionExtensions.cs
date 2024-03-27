using Npgsql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using WeatherForecastsArchivesViewer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using WeatherForecastsArchivesViewer.Application.Interfaces.Persistence;
using WeatherForecastsArchivesViewer.Infrastructure.Persistence.Repositories;
using WeatherForecastsArchivesViewer.Application.Interfaces.Persistence.Repositories;
using PigTrade.DataPlant.Abstractions.Ports.Database;

namespace WeatherForecastsArchivesViewer.Infrastructure;

/// <summary>
/// Статический класс расширений для <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление сервисов для работы с базой данных.
    /// </summary>
    public static void AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>();
        services.AddScoped<IDatabasePort, DatabaseAdapter>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
    }

    /// <summary>
    /// Накатывает миграции.
    /// </summary>
    public static void ApplyMigrations(this IServiceProvider serviceProvider, IConfiguration configuration, ILogger logger)
    {
        logger.LogInformation("Применение миграций...");

        using var scope = serviceProvider.CreateScope();

        try
        {
            var conString = configuration.GetConnectionString("WeatherForecasts");
            if (string.IsNullOrWhiteSpace(conString))
            {
                throw new Exception("Не задана connection string");
            }

            logger.LogInformation($"Connection string: {conString}");

            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            if (db.Database.GetPendingMigrations().Any())
            {
                db.Database.Migrate();
            }
            logger.LogInformation("Успешное применение миграций");
        }
        catch (NpgsqlException ex)
        {
            if (ex.InnerException is SocketException socketException)
            {
                logger.LogError(ex, "Не удалось установить соединение с БД");
                throw new Exception("Принудительное завершение сервиса");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Не удалось применить миграции", ex);
        }
    }
}
