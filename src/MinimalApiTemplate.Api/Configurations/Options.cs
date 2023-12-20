using MinimalApiTemplate.Application.Common.Settings;

namespace MinimalApiTemplate.Api.Configurations;

public static class Options
{
    public static void ConfigureSettings(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<AppSettings>(config);

        services.Configure<MassTransitSettings>(config.GetSection("MassTransit"));
    }
}
