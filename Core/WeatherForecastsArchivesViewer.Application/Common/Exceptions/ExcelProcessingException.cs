namespace WeatherForecastsArchivesViewer.Application.Common.Exceptions;

/// <summary>
/// Ошибка при обработке Excel файла.
/// </summary>
public class ExcelProcessingException : Exception
{
    public ExcelProcessingException() : base() { }

    public ExcelProcessingException(string message) : base(message) { }

    public ExcelProcessingException(string message, Exception innerException) : base(message, innerException) { }
}
