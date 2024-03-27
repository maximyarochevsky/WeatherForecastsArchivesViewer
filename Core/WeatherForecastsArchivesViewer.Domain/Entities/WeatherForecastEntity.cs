using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeatherForecastsArchivesViewer.Domain.Enums;

namespace WeatherForecastsArchivesViewer.Domain.Entities;

[Table("weather_forecasts")]
[PrimaryKey(nameof(Id))]
/// <summary>
/// Сущность прогноза погоды.
/// </summary>
public class WeatherForecastEntity
{
    private string? _id;

    /// <summary>
    /// Уникальный составной ID прогноза.
    /// </summary>
    public string Id
    {
        get => _id ??= $"{Date}_{Time}";
        set => _id = value;
    }

    /// <summary>
    /// Дата без времени суток.
    /// </summary>
    [Column("date")]
    public DateOnly Date { get; set; }

    /// <summary>
    /// Время суток.
    /// </summary>
    [Column("time")]
    public TimeOnly Time { get; set; }

    /// <summary>
    /// Температура воздуха.
    /// </summary>
    [Column("temperature")]
    public float Temperature { get; set; }

    /// <summary>
    /// Влажность воздуха.
    /// </summary>
    [Column("air_humidity")]
    public float AirHumidity { get; set; }

    /// <summary>
    /// Точка росы.
    /// </summary>
    [Column("dew_point")]
    public float DewPoint { get; set; }

    /// <summary>
    /// Атмосферное давление.
    /// </summary>
    [Column("atmospheric_pressure")]
    public int AtmosphericPressure { get; set; }

    /// <summary>
    /// Направление ветра.
    /// </summary>
    [Column("wind_direction")]
    [EnumDataType(typeof(WindDirection))]
    public WindDirection? WindDirection { get; set; }

    /// <summary>
    /// Скорость ветра.
    /// </summary>
    [Column("wind_speed")]
    public int? WindSpeed { get; set; }

    /// <summary>
    /// Облачность.
    /// </summary>
    [Column("cloud_cover")]
    public int? CloudCover { get; set; }

    /// <summary>
    
    /// </summary>
    [Column("lower_limit_cloud_cover")]
    public int? LowerLimitCloudCover { get; set; }

    /// <summary>
    /// Горизонтальная видимость.
    /// </summary>
    [Column("horizontal_visibility")]
    public int? HorizontalVisibility { get; set; }

    /// <summary>
    /// Погодное явление.
    /// </summary>
    [Column("weather_event")]
    public string? WeatherEvent { get; set; }
}
