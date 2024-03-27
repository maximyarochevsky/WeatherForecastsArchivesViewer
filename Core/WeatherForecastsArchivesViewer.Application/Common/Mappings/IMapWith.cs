using AutoMapper;

namespace WeatherForecastsArchivesViewer.Application.Common.Mappings;

/// <summary>
/// Интерфейс для определения метода настройки маппинга между типами.
/// </summary>
public interface IMapWith<T>
{
    /// <summary>
    /// Метод для настройки маппинга с AutoMapper, создает маппинг между типом T и типом,
    /// реализующим интерфейс.
    /// </summary>
    void Mapping(Profile profile) =>
        profile.CreateMap(typeof(T), GetType());
}
