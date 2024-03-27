using MediatR;
using WeatherForecastsArchivesViewer.Application.WeatherForecast.DTO;

namespace WeatherForecastsArchivesViewer.Application.HourlyWeatherForecast.Queries.GetAllForecastsQuery;

/// <summary>
/// Запрос всех данных по прогнозам погоды.
/// </summary>
public class GetAllForecastsQuery : IRequest<List<WeatherForecastDto>>
{
}
