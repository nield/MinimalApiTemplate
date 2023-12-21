using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MinimalApiTemplate.Worker.Configurations;

[ExcludeFromCodeCoverage]
public static class MassTransit
{
    public static void SetupMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        if (!configuration.GetValue<bool>("MassTransit:ConsumerEnabled")) return;

        services.AddMassTransit(config =>
        {
            config.AddConsumers(Assembly.GetEntryAssembly());

            config.UsingRabbitMq((context, rabbitConfig) =>
            {
                var rabbitUri = configuration["MassTransit:RabbitMq:Uri"];

                ArgumentException.ThrowIfNullOrWhiteSpace(rabbitUri);

                rabbitConfig.Host(new Uri(rabbitUri));

                rabbitConfig.ConfigureEndpoints(context);
            });
        });
    }
}

