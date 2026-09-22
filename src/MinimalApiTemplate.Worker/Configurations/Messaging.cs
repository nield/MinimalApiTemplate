using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using MinimalApiTemplate.Worker.Consumers.V1.TodoItems;
using Rebus.Config;

namespace MinimalApiTemplate.Worker.Configurations;

[ExcludeFromCodeCoverage]
public static class Messaging
{
    public static void SetupMessaging(this IHostApplicationBuilder builder)
    {
        if (!builder.Configuration.GetValue<bool>("Messaging:ConsumerEnabled")) return;

        var rabbitUri = builder.Configuration.GetConnectionString("RabbitMq");

        ArgumentException.ThrowIfNullOrWhiteSpace(rabbitUri);

        builder.Services.AutoRegisterHandlersFromAssemblyOf<ToDoItemCreatedConsumer>();

        var messageTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .SelectMany(t => t.GetInterfaces())
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandleMessages<>))
            .Select(i => i.GetGenericArguments()[0])
            .Distinct()
            .ToArray();

        builder.Services.AddRebus(
            configure => configure
                .Transport(t => t.UseRabbitMq(rabbitUri, "minimalapitemplate-worker"))
                .Options(o => o.EnableDiagnosticSources()),
            onCreated: bus =>
                Task.WhenAll(messageTypes.Select(bus.Subscribe)));

        builder.AddRabbitMQClient(connectionName: "RabbitMq");
    }
}
