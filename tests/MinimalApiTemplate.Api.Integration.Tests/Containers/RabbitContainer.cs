using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace MinimalApiTemplate.Api.Integration.Tests.Containers;

internal sealed class RabbitContainer
{
    private const ushort RabbitDefaultPort = 5672;
    private const string Username = "test";
    private const string Password = "test";
    private readonly IContainer _rabbitContainer;

    private static readonly Lazy<RabbitContainer> SingleLazyInstance = new(() => new RabbitContainer());

    public static RabbitContainer Instance => SingleLazyInstance.Value;

    public string GetRabbitConnectionString() => $"amqp://{Username}:{Password}@{_rabbitContainer!.Hostname}:{_rabbitContainer.GetMappedPublicPort(RabbitDefaultPort)}";

    private RabbitContainer()
    {
        var rabbit = new ContainerBuilder()
                  .WithImage("rabbitmq:alpine")
                  .WithPortBinding(RabbitDefaultPort, true)
                  .WithEnvironment("RABBITMQ_DEFAULT_USER", Username)
                  .WithEnvironment("RABBITMQ_DEFAULT_PASS", Password)
                  .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(RabbitDefaultPort))
                  .Build();

        _rabbitContainer = rabbit;

        rabbit.StartAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        var seconds = 0;

        while (rabbit.State != TestcontainersStates.Running)
        {
            Thread.Sleep(1000);

            seconds += 1000;

            if (seconds >= 10000)
            {
                throw new OperationCanceledException("The containers did not start in time");
            }
        }
    }
}
