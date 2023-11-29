using MinimalApiTemplate.Api.Services;
using MinimalApiTemplate.Application.Common.Interfaces;

namespace MinimalApiTemplate.Api.Configurations;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpointsApiExplorer();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddSingleton(TimeProvider.System);

        services.AddHttpContextAccessor();

        services.ConfigureHealthChecks(config);

        services.ConfigureFluentValidator();

        services.ConfigureAutoMapper();

        services.ConfigureExceptionHandlers();

        services.AddApiEndpoints();

        services.ConfigureSwagger(config);

        services.ConfigureVersioning();

        services.ConfigureSettings(config);

        services.ConfigureCompression();

        return services;
    }
}
