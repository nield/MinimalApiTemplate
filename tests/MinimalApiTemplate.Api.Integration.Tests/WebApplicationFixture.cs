using System.Net.Http.Headers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using MinimalApiTemplate.Api.Integration.Tests.Containers;
using MinimalApiTemplate.Infrastructure.Persistence;
using Respawn;

namespace MinimalApiTemplate.Api.Integration.Tests;

public class WebApplicationFixture : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory<global::Program> _factory = new();

    private SqlConnection? _databaseConnection = null;
    private Respawner? _respawner = null;
    private HttpClient? _httpClient = null;

    public HttpClient HttpClient
    {
        get
        {
            if (_httpClient is null)
            {
                throw new NullReferenceException("HttpClient not set");
            }

            return _httpClient;
        }
    }

    public async Task InitializeAsync()
    {
        await StartContainers();

        _httpClient = _factory.CreateClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

        _databaseConnection = new SqlConnection(DatabaseContainer.Instance.GetConnectionString());
        await _databaseConnection.OpenAsync();

        _respawner = await Respawner.CreateAsync(_databaseConnection, new RespawnerOptions
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
            using var cancellationSource = new CancellationTokenSource(TimeSpan.FromSeconds(60));

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
        if (_respawner is not null && _databaseConnection is not null)
        {
            await _respawner.ResetAsync(_databaseConnection);
        }

        using var scope = _factory.Services.CreateScope();

        var dbContextInitialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await dbContextInitialiser.MigrateDatabaseAsync();
        await dbContextInitialiser.SeedDataAsync();
    }
        
    public async Task DisposeAsync()
    {
        _httpClient?.Dispose();

        if (_databaseConnection is not null)
        {
            await _databaseConnection.DisposeAsync();
        }
    }
}
