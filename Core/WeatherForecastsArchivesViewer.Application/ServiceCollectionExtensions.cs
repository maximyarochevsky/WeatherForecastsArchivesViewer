using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WeatherForecastsArchivesViewer.Application.Common.Mappings;

namespace WeatherForecastsArchivesViewer.Application;

/// <summary>
/// Статический класс расширений для <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрация сервисов из Application.
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection
        services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        });

        return services;
    }
}
