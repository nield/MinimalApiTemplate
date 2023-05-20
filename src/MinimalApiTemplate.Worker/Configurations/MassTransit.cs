using System.Reflection;

namespace MinimalApiTemplate.Worker.Configurations;

public static class MassTransit
{
    public static void SetupMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        if (!configuration.GetValue<bool>("MassTransit:ConsumerEnabled")) return;

        services.AddMassTransit(config =>
        {
            config.AddConsumers(new[] { Assembly.GetEntryAssembly() });

            config.UsingRabbitMq((context, rabbitConfig) =>
            {
                var rabbitUri = configuration["MassTransit:RabbitMq:Uri"];

                rabbitConfig.Host(new Uri(rabbitUri));

                rabbitConfig.ConfigureEndpoints(context);
            });
        });
    }
}
