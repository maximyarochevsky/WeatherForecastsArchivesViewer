namespace WeatherForecastsArchivesViewer.Application.Common.Exceptions;

/// <summary>
/// Исключение, возникающее при некорректных или отсутствующих данных в строке Excel-листа.
/// </summary>
public class InvalidRowDataException : Exception
{
    public InvalidRowDataException() : base() { }

    public InvalidRowDataException(string message) : base(message) { }

    public InvalidRowDataException(string message, Exception innerException) : base(message, innerException) { }
}
