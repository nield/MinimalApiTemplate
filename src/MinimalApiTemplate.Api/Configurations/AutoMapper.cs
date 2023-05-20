using System.Reflection;
using MinimalApiTemplate.Application.Common.Interfaces;

namespace MinimalApiTemplate.Api.Configurations;
public static class AutoMapper
{
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(new[]
{
            Assembly.GetExecutingAssembly(),
            typeof(IApplicationMarker).Assembly
        });
    }
}
