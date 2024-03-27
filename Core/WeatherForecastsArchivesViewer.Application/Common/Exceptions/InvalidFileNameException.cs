namespace WeatherForecastsArchivesViewer.Application.Common.Exceptions;

/// <summary>
/// Исключение, возникающее при некорректном имени файла.
/// </summary>
public class InvalidFileNameException : Exception
{
    public InvalidFileNameException() : base() { }

    public InvalidFileNameException(string message) : base(message) { }

    public InvalidFileNameException(string message, Exception innerException) : base(message, innerException) { }
}
