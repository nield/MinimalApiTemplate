namespace MinimalApiTemplate.Worker.Configurations;

public static class ConfigureServices
{
    public static IServiceCollection SetupServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.SetupMassTransit(configuration);

        return services;
    }
}
