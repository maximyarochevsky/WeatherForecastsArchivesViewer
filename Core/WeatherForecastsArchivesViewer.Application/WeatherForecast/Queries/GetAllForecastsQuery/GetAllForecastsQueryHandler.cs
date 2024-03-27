using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeatherForecastsArchivesViewer.Application.Interfaces.Persistence;
using WeatherForecastsArchivesViewer.Application.WeatherForecast.DTO;

namespace WeatherForecastsArchivesViewer.Application.WeatherForecast.Queries.GetAllForecastsQuery;

/// <summary>
/// Обработчик запроса всех данных по прогнозам погоды.
/// </summary>
public class GetAllForecastsQueryHandler : IRequestHandler<GetAllForecastsQuery, List<WeatherForecastDto>>
{
    /// <summary>
    /// Логгер для ведения журнала работы программы.
    /// </summary>
    private readonly ILogger<GetAllForecastsQueryHandler> _logger;

    /// <summary>
    /// Маппер для преобразования объектов.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Провайдер сервисов.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    public GetAllForecastsQueryHandler(
        ILogger<GetAllForecastsQueryHandler> logger,
        IMapper mapper,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _mapper = mapper;
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc cref="IRequestHandler{TRequest, TResponse}.Handle(TRequest, CancellationToken)"/>
    public async Task<List<WeatherForecastDto>> Handle(GetAllForecastsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var databaseProvider = scope.ServiceProvider.GetRequiredService<IDatabasePort>();

            var allForecastsDb = await databaseProvider.Execute(async u => await u.WeatherForecast.GetAllForecasts(cancellationToken));

            var allForecastDTO = _mapper.Map<List<WeatherForecastDto>>(allForecastsDb);

            return allForecastDTO;
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Error when requesting all forecast data.");
            throw new Exception("Ошибка при обработке запроса на получение данных по прогнозам.");
        }
    }
}
