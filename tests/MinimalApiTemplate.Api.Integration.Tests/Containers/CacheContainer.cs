using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace MinimalApiTemplate.Api.Integration.Tests.Containers;

internal sealed class CacheContainer : BaseContainer<CacheContainer>
{
    private const ushort CacheDefaultPort = 6379;

    public string GetCacheConnectionString() => $"{_container!.Hostname}:{_container.GetMappedPublicPort(CacheDefaultPort)}";

    protected override IContainer BuildContainer()
    {
        return new ContainerBuilder("redis:latest")
           .WithPortBinding(CacheDefaultPort, true)
           .WithWaitStrategy(Wait.ForUnixContainer().UntilInternalTcpPortIsAvailable(CacheDefaultPort))
           .Build();
    }

    public override string GetConnectionString() =>
        $"{_container!.Hostname}:{_container.GetMappedPublicPort(CacheDefaultPort)}";
}
