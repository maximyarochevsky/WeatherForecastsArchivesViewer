using MediatR;
using Microsoft.AspNetCore.Http;

namespace WeatherForecastsArchivesViewer.Application.WeatherForecast.Commands.UploadWeatherArchiveCommand;

/// <summary>
/// Запрос на загрузку архива(-ов) с прогнозами.
/// </summary>
public record UploadWeatherArchive
    (ICollection<IFormFile> files) : IRequest<bool>;
