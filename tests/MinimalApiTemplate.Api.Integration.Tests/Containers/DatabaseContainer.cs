using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Data.SqlClient;

namespace MinimalApiTemplate.Api.Integration.Tests.Containers;

internal sealed class DatabaseContainer
{
    private const string DatabaseName = "templateDb";
    private const string DatabaseUsername = "sa";
    private const string DatabasePassword = "yourStrong(!)Password";
    private const ushort DatabaseDefaultPort = 1433;

    private readonly IContainer _databaseContainer;

    private static readonly Lazy<DatabaseContainer> SingleLazyInstance = new(() => new DatabaseContainer());

    public static DatabaseContainer Instance => SingleLazyInstance.Value;

    public string GetDatabaseConnectionString() => $"Server={_databaseContainer!.Hostname},{_databaseContainer.GetMappedPublicPort(DatabaseDefaultPort)};Database={DatabaseName};User Id={DatabaseUsername};Password={DatabasePassword};TrustServerCertificate=True";

    private DatabaseContainer()
    {
        _databaseContainer = new ContainerBuilder()
               .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
               .WithPortBinding(DatabaseDefaultPort, true)
               .WithEnvironment("ACCEPT_EULA", "Y")
               .WithEnvironment("MSSQL_SA_PASSWORD", DatabasePassword)
               .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(DatabaseDefaultPort))
               .Build();

        _databaseContainer.StartAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        var seconds = 0;

        while (!IsServerConnected())
        {
            Thread.Sleep(1000);

            seconds += 1000;

            if (seconds >= 100000)
            {
                throw new OperationCanceledException("The containers did not start in time");
            }
        }
    }

    private bool IsServerConnected()
    {
        using var connection = new SqlConnection($"Server={_databaseContainer!.Hostname},{_databaseContainer?.GetMappedPublicPort(DatabaseDefaultPort)};Database=master;User Id={DatabaseUsername};Password={DatabasePassword};TrustServerCertificate=True");

        try
        {
            connection.Open();
            return true;
        }
        catch (SqlException)
        {
            return false;
        }
    }
}
