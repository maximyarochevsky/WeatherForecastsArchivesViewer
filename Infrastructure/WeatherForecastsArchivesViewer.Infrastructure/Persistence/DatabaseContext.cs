using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherForecastsArchivesViewer.Domain.Entities;
using WeatherForecastsArchivesViewer.Domain.Enums;

namespace WeatherForecastsArchivesViewer.Infrastructure.Persistence;

/// <summary>
/// Контекст базы данных.
/// </summary>
public class DatabaseContext : DbContext 
{
    #region Fields

    /// <summary>
    /// Конфигурация приложения.
    /// </summary>
    private readonly IConfiguration _configuration;

    #endregion

    #region .ctor

    public DatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;       
    }

    #endregion

    #region Tables

    /// <inheritdoc cref="WeatherForecastEntity""/>
    public DbSet<WeatherForecastEntity> WeatherForecasts => Set<WeatherForecastEntity>();

    #endregion

    #region Overrides

    /// <inheritdoc cref="DbContext.OnConfiguring(DbContextOptionsBuilder)"/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("WeatherForecasts"));
    }

    /// <inheritdoc cref="DbContext.ConfigureConventions(ModelConfigurationBuilder)"/>
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder);
    }

    /// <inheritdoc cref="DbContext.OnModelCreating(ModelBuilder)"/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<WeatherForecastEntity>()
            .Property(e => e.WindDirection)
            .HasConversion(
                v => v.ToString(),
                v => (WindDirection)Enum.Parse(typeof(WindDirection), v)
            );
        base.OnModelCreating(modelBuilder);
    }

    #endregion
}
