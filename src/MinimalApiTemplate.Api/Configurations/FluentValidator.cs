using System.Reflection;
using FluentValidation;
using MinimalApiTemplate.Application.Common.Interfaces;

namespace MinimalApiTemplate.Api.Configurations;

public static class FluentValidator
{
    public static void ConfigureFluentValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(new[]
{
            Assembly.GetExecutingAssembly(),
            typeof(IApplicationMarker).Assembly
        }, ServiceLifetime.Singleton);
    }
}
