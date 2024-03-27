using WeatherForecastsArchivesViewer.Domain.Enums;

namespace WeatherForecastsArchivesViewer.Application.WeatherForecast.DTO;

/// <summary>
/// Прогноз погоды.
/// </summary>
public record WeatherForecastDto
    (
    DateOnly Date,
    TimeOnly Time,
    float Temperature,
    float AirHumidity,
    float DewPoint,
    int AtmosphereicPressure,
    WindDirection? WindDirection,
    int? WindSpeed,
    int? CloudCover,
    int? LowerLimitCloudCover,
    int? HorizontalVisibility,
    string? WeatherEvent
    );
