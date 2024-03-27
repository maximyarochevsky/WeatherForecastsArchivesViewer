using Microsoft.Extensions.DependencyInjection;
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

        return services;
    }
}
