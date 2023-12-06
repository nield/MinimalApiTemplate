using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using MinimalApiTemplate.Api.Integration.Tests.Containers;

namespace MinimalApiTemplate.Api.Integration.Tests;

internal sealed class CacheContainer : BaseContainer<CacheContainer>
{
    private const ushort CacheDefaultPort = 6379;

    public string GetCacheConnectionString() => $"{_container!.Hostname}:{_container.GetMappedPublicPort(CacheDefaultPort)}";

    protected override IContainer BuildContainer()
    {
        return new ContainerBuilder()
           .WithImage("redis:latest")
           .WithPortBinding(CacheDefaultPort, true)
           .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(CacheDefaultPort))
           .Build();
    }

    public override string GetConnectionString() =>
        $"{_container!.Hostname}:{_container.GetMappedPublicPort(CacheDefaultPort)}";
}
