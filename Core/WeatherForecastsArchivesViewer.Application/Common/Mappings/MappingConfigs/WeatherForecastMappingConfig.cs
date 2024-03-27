using AutoMapper;
using WeatherForecastsArchivesViewer.Application.WeatherForecast.DTO;
using WeatherForecastsArchivesViewer.Domain.Entities;

namespace WeatherForecastsArchivesViewer.Application.Common.Mappings.MappingConfigs;

/// <summary>
/// Конфигурация для маппинга между сущностью и DTO прогноза погоды.
/// </summary>
public class WeatherForecastMappingConfig : IMapWith<WeatherForecastDto>
{

    /// <inheritdoc cref="IMapWith{T}.Mapping(Profile)"/>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WeatherForecastEntity, WeatherForecastDto>()
            .ForCtorParam(ctorParamName: "Date", 
               opt => opt.MapFrom(weatherForecastEntity => weatherForecastEntity.Date))
            .ForCtorParam(ctorParamName: "Time",
               opt => opt.MapFrom(weatherForecastEntity => weatherForecastEntity.Time))
            .ForCtorParam(ctorParamName: "Temperature",
                opt => opt.MapFrom(weatherForecastEntity => weatherForecastEntity.Temperature))
            .ForCtorParam(ctorParamName: "AirHumidity",
                opt => opt.MapFrom(weatherForecastEntity => weatherForecastEntity.AirHumidity))
            .ForCtorParam(ctorParamName: "Dewpoint",
                opt => opt.MapFrom(weatherForecastEntity => weatherForecastEntity.DewPoint))
            .ForCtorParam(ctorParamName: "AtmosphereicPressure",
                opt => opt.MapFrom(weatherForecastEntity => weatherForecastEntity.AtmosphericPressure))
            .ForCtorParam(ctorParamName: "WindDirection",
                opt => opt.MapFrom(weatherForecastEntity => weatherForecastEntity.WindDirection))
            .ForCtorParam(ctorParamName: "WindSpeed",
                opt => opt.MapFrom(weatherForecastEntity => weatherForecastEntity.WindSpeed))
            .ForCtorParam(ctorParamName: "CloudCover",
                opt => opt.MapFrom(weatherForecastEntity => weatherForecastEntity.CloudCover))
            .ForCtorParam(ctorParamName: "LowerLimitCloudCover",
                opt => opt.MapFrom(weatherForecastEntity => weatherForecastEntity.LowerLimitCloudCover))
            .ForCtorParam(ctorParamName: "HorizontalVisibility",
                opt => opt.MapFrom(weatherForecastEntity => weatherForecastEntity.HorizontalVisibility))
            .ForCtorParam(ctorParamName: "WeatherEvent",
                opt => opt.MapFrom(weatherForecastEntity => weatherForecastEntity.WeatherEvent));

        profile.CreateMap<WeatherForecastDto, WeatherForecastEntity>()
           .ForMember(weatherForecastEntity => weatherForecastEntity.Id,
               opt => opt.MapFrom(weatherForecast => $"{weatherForecast.Date}_{weatherForecast.Time}"))
           .ForMember(weatherForecastEntity => weatherForecastEntity.Date,
               opt => opt.MapFrom(weatherForecast => weatherForecast.Date))
           .ForMember(weatherForecastEntity => weatherForecastEntity.Time,
               opt => opt.MapFrom(weatherForecast => weatherForecast.Time))
           .ForMember(weatherForecastEntity => weatherForecastEntity.Temperature,
               opt => opt.MapFrom(weatherForecast => weatherForecast.Temperature))
           .ForMember(weatherForecastEntity => weatherForecastEntity.AirHumidity,
               opt => opt.MapFrom(weatherForecast => weatherForecast.AirHumidity))
           .ForMember(weatherForecastEntity => weatherForecastEntity.DewPoint,
               opt => opt.MapFrom(weatherForecast => weatherForecast.DewPoint))
           .ForMember(weatherForecastEntity => weatherForecastEntity.AtmosphericPressure,
               opt => opt.MapFrom(weatherForecast => weatherForecast.AtmosphereicPressure))
           .ForMember(weatherForecastEntity => weatherForecastEntity.WindDirection,
               opt => opt.MapFrom(weatherForecast => weatherForecast.WindDirection))
           .ForMember(weatherForecastEntity => weatherForecastEntity.WindSpeed,
               opt => opt.MapFrom(weatherForecast => weatherForecast.WindSpeed))
           .ForMember(weatherForecastEntity => weatherForecastEntity.CloudCover,
               opt => opt.MapFrom(weatherForecast => weatherForecast.CloudCover))
           .ForMember(weatherForecastEntity => weatherForecastEntity.LowerLimitCloudCover,
               opt => opt.MapFrom(weatherForecast => weatherForecast.LowerLimitCloudCover))
           .ForMember(weatherForecastEntity => weatherForecastEntity.HorizontalVisibility,
               opt => opt.MapFrom(weatherForecast => weatherForecast.HorizontalVisibility))
           .ForMember(weatherForecastEntity => weatherForecastEntity.WeatherEvent,
               opt => opt.MapFrom(weatherForecast => weatherForecast.WeatherEvent));
    }
}
