using AutoMapper;
using System.Reflection;

namespace WeatherForecastsArchivesViewer.Application.Common.Mappings;

/// <summary>
/// Настройка сопоставления типов внутри сборки.
/// </summary>
public class AssemblyMappingProfile : Profile
{
    public AssemblyMappingProfile(Assembly assembly) =>
        ApplyMappingsFromAssembly(assembly);

    /// <summary>
    /// Поиск всех типов, реализующих интерфейс IMapWith<>, для настройки сопоставления.
    /// </summary>
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType &&
                          i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping");
            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}