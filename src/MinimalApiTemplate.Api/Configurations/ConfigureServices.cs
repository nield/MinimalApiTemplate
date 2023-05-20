using MinimalApi.Endpoint.Extensions;
using MinimalApiTemplate.Api.Filters;
using MinimalApiTemplate.Api.Services;
using MinimalApiTemplate.Application.Common.Interfaces;


namespace MinimalApiTemplate.Api.Configurations;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpointsApiExplorer();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<ExceptionHandlingMiddleware>();

        services.AddHttpContextAccessor();

        services.ConfigureHealthChecks(config);

        services.ConfigureFluentValidator();

        services.ConfigureAutoMapper();       

        services.AddEndpoints();

        services.ConfigureSwagger(config);

        services.ConfigureVersioning();

        services.ConfigureSettings(config);

        services.ConfigureCompression();

        return services;
    }
}
