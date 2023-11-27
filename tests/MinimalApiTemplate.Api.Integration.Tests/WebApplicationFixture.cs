using System.Net.Http.Headers;
using MinimalApiTemplate.Api.Integration.Tests.Containers;
using MinimalApiTemplate.Infrastructure.Persistence;
using Respawn;

namespace MinimalApiTemplate.Api.Integration.Tests;

public class WebApplicationFixture : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory<global::Program> _factory = new();

    private string? _databaseConnectionString = null;

    public HttpClient HttpClient { get; private set; } = null!;
    public Respawner Respawner { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        await Task.WhenAll(
            DatabaseContainer.Instance.StartContainerAsync(),
            CacheContainer.Instance.StartContainerAsync(),
            RabbitContainer.Instance.StartContainerAsync());

        HttpClient = _factory.CreateClient();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

        _databaseConnectionString = DatabaseContainer.Instance.GetConnectionString();

        Respawner = await Respawner.CreateAsync(_databaseConnectionString, new RespawnerOptions
        {
            SchemasToInclude = [ApplicationDbContext.DbSchema],
            TablesToIgnore = [ApplicationDbContext.MigrationTableName]
        });
    }

    public async Task ResetDatabaseAsync() => 
        await Respawner.ResetAsync(_databaseConnectionString!);

    public Task DisposeAsync()
    {
        HttpClient.Dispose();

        return Task.CompletedTask;
    }
}
