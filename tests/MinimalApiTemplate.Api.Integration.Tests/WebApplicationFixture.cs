using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
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
        await StartContainers();

        HttpClient = _factory.CreateClient();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

        _databaseConnectionString = DatabaseContainer.Instance.GetConnectionString();

        Respawner = await Respawner.CreateAsync(_databaseConnectionString, new RespawnerOptions
        {
            SchemasToInclude = [ApplicationDbContext.DbSchema],
            TablesToIgnore = [ApplicationDbContext.MigrationTableName],
            WithReseed = true
        });
    }

    private static async Task StartContainers()
    {
        try
        {
            using var cancellationSource = new CancellationTokenSource(TimeSpan.FromSeconds(20));

            await Task.WhenAll(
                DatabaseContainer.Instance.StartContainerAsync(cancellationSource.Token),
                CacheContainer.Instance.StartContainerAsync(cancellationSource.Token),
                RabbitContainer.Instance.StartContainerAsync(cancellationSource.Token));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public async Task ResetDatabaseAsync()
    {
        await Respawner.ResetAsync(_databaseConnectionString!);

        using var scope = _factory.Services.CreateScope();

        var dbContextInitialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await dbContextInitialiser.MigrateDatabaseAsync();
        await dbContextInitialiser.SeedDataAsync();
    }
        

    public Task DisposeAsync()
    {
        HttpClient.Dispose();

        return Task.CompletedTask;
    }
}
