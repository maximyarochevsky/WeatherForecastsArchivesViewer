using WeatherForecastsArchivesViewer.Domain.Enums;

namespace WeatherForecastsArchivesViewer.Application.Common.Helpers;

/// <summary>
/// Класс для получения направлений ветра.
/// </summary>
public static class WindDirectionParser
{
    private static readonly Dictionary<WindDirection, string> RussianWindDirections = new Dictionary<WindDirection, string>
    {
        { WindDirection.North, "С" },
        { WindDirection.South, "Ю" },
        { WindDirection.West, "З" },
        { WindDirection.East, "В" },
        { WindDirection.Northwest, "СЗ" },
        { WindDirection.Northeast, "СВ" },
        { WindDirection.Southwest, "ЮЗ" },
        { WindDirection.Southeast, "ЮВ" },
        { WindDirection.NorthNorthwest, "С,СЗ" },
        { WindDirection.NorthNortheast, "С,СВ" },
        { WindDirection.SouthSouthwest, "Ю,ЮЗ" },
        { WindDirection.SouthSoutheast, "Ю,ЮВ" },
        { WindDirection.WestSouthwest, "З,ЮЗ" },
        { WindDirection.EastSoutheast, "В,ЮВ" },
        { WindDirection.WestNorthwest, "З,СЗ" },
        { WindDirection.EastNortheast, "В,СВ" },
        { WindDirection.Calm, "штиль" }
    };

    /// <summary>
    /// Возвращает название направления на русском.
    /// </summary>
    public static string? GetRussianWindDirection(WindDirection? direction)
    {
        if (direction.HasValue && RussianWindDirections.ContainsKey(direction.Value))
        {
            return RussianWindDirections[direction.Value];
        }
        else
        {
            return null;
        }
    }
}
