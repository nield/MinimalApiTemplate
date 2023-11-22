using System.Net.Http.Headers;
using MinimalApiTemplate.Api.Integration.Tests.Containers;
using Respawn;

namespace MinimalApiTemplate.Api.Integration.Tests;
public class WebApplicationFixture : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory<global::Program> _factory = new();

    public HttpClient HttpClient { get; private set; }
    public Respawner Respawner { get; private set; }

    public async Task InitializeAsync()
    {
        await Task.WhenAll(
            DatabaseContainer.Instance.StartContainer(),
            CacheContainer.Instance.StartContainer(),
            RabbitContainer.Instance.StartContainer());

        HttpClient = _factory.CreateClient();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

        Respawner = await Respawner.CreateAsync(DatabaseContainer.Instance.GetConnectionString());
    }

    public Task DisposeAsync()
    {
        HttpClient.Dispose();

        return Task.CompletedTask;
    }
}
