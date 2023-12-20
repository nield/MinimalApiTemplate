using System.Diagnostics.CodeAnalysis;

namespace MinimalApiTemplate.Worker.Configurations;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static IServiceCollection SetupWorker(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.SetupMassTransit(configuration);

        return services;
    }
}

