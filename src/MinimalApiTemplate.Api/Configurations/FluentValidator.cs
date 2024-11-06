using System.Reflection;
using MinimalApiTemplate.Application.Common.Interfaces;

namespace MinimalApiTemplate.Api.Configurations;

public static class FluentValidator
{
    public static void ConfigureFluentValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(
[
            Assembly.GetExecutingAssembly(),
            typeof(IApplicationMarker).Assembly
        ], ServiceLifetime.Singleton);
    }
}
