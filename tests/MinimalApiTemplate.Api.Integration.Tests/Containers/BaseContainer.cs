using DotNet.Testcontainers.Containers;

namespace MinimalApiTemplate.Api.Integration.Tests.Containers;

internal abstract class BaseContainer<TContainer>
    where TContainer : class, new()
{
    private static readonly Lazy<TContainer> SingleLazyInstance = new(() => new TContainer());

    public static TContainer Instance => SingleLazyInstance.Value;

    protected readonly IContainer _container;

    protected BaseContainer()
    {
        _container = BuildContainer();
    }

    protected abstract IContainer BuildContainer();

    public abstract string GetConnectionString();

    public async virtual Task StartContainerAsync(int millisecondsTimeout = 10000)
    {
        await _container.StartAsync();

        var containerRunning = IsContainerRunning(millisecondsTimeout);

        if (!containerRunning)
        {
            throw new OperationCanceledException("The containers did not start in time");
        }
    }

    private bool IsContainerRunning(int millisecondsTimeout)
    {
        var seconds = 0;

        while (_container.State != TestcontainersStates.Running)
        {
            Thread.Sleep(1000);

            seconds += 1000;

            if (seconds >= millisecondsTimeout) return false;
        }

        return true;
    }
}
