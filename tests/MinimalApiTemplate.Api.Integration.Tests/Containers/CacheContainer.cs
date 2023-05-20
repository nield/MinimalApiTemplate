using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace MinimalApiTemplate.Api.Integration.Tests;

internal sealed class CacheContainer
{
    private const ushort CacheDefaultPort = 6379;
    private readonly IContainer _cacheContainer;

    private static readonly Lazy<CacheContainer> SingleLazyInstance = new(() => new CacheContainer());

    public static CacheContainer Instance => SingleLazyInstance.Value;

    public string GetCacheConnectionString() => $"{_cacheContainer!.Hostname}:{_cacheContainer.GetMappedPublicPort(CacheDefaultPort)}";

    private CacheContainer() 
    {
        var redis = new ContainerBuilder()
                   .WithImage("redis:latest")
                   .WithPortBinding(CacheDefaultPort, true)
                   .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(CacheDefaultPort))
                   .Build();

        _cacheContainer = redis;

        redis.StartAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        var seconds = 0;

        while (redis.State != TestcontainersStates.Running)
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
