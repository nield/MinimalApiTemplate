using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MinimalApiTemplate.Worker.Configurations;

[ExcludeFromCodeCoverage]
public static class MassTransit
{
    public static void SetupMassTransit(this IHostApplicationBuilder builder)
    {
        if (!builder.Configuration.GetValue<bool>("MassTransit:ConsumerEnabled")) return;

        builder.Services.AddMassTransit(config =>
        {
            config.AddConsumers(Assembly.GetEntryAssembly());

            config.UsingRabbitMq((context, rabbitConfig) =>
            {
                var rabbitUri = builder.Configuration.GetConnectionString("RabbitMq");

                ArgumentException.ThrowIfNullOrWhiteSpace(rabbitUri);

                rabbitConfig.Host(new Uri(rabbitUri));

                rabbitConfig.ConfigureEndpoints(context);
            });
        });
    }
}

