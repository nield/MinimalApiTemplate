using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace MinimalApiTemplate.Api.Integration.Tests.Containers;

internal sealed class RabbitContainer : BaseContainer<RabbitContainer>
{
    private const ushort RabbitDefaultPort = 5672;
    private const string Username = "test";
    private const string Password = "test";

    protected override IContainer BuildContainer()
    {
        return new ContainerBuilder()
            .WithImage("rabbitmq:alpine")
            .WithPortBinding(RabbitDefaultPort, true)
            .WithEnvironment("RABBITMQ_DEFAULT_USER", Username)
            .WithEnvironment("RABBITMQ_DEFAULT_PASS", Password)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(RabbitDefaultPort))
            .Build();
    }

    public override string GetConnectionString() =>
         $"amqp://{Username}:{Password}@{_container!.Hostname}:{_container.GetMappedPublicPort(RabbitDefaultPort)}";
}
