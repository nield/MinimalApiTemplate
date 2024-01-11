using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Data.SqlClient;

namespace MinimalApiTemplate.Api.Integration.Tests.Containers;

internal sealed class DatabaseContainer : BaseContainer<DatabaseContainer>
{
    private const string DatabaseName = "templateDb";
    private const string DatabaseUsername = "sa";
    private const string DatabasePassword = "yourStrong(!)Password";
    private const ushort DatabaseDefaultPort = 1433;

    protected override IContainer BuildContainer()
    {
        return new ContainerBuilder()
           .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
           .WithPortBinding(DatabaseDefaultPort, true)
           .WithEnvironment("ACCEPT_EULA", "Y")
           .WithEnvironment("MSSQL_SA_PASSWORD", DatabasePassword)
           .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(DatabaseDefaultPort))
           .Build();
    }

    public override string GetConnectionString() =>
        $"Server={_container!.Hostname},{_container.GetMappedPublicPort(DatabaseDefaultPort)};Database={DatabaseName};User Id={DatabaseUsername};Password={DatabasePassword};TrustServerCertificate=True";

    public override async Task StartContainerAsync(int millisecondsTimeout = 10000)
    {
        await base.StartContainerAsync(millisecondsTimeout);

        if (!await IsServerConnectedAsync(millisecondsTimeout))
        {
            throw new OperationCanceledException("The container connection was not open in time");
        }
    }

    private async Task<bool> IsServerConnectedAsync(int millisecondsTimeout)
    {
        var dbConnectionString = GetConnectionString();

        var dbMasterConnectionString = dbConnectionString.Replace(DatabaseName, "master");

        using var connection = new SqlConnection(dbMasterConnectionString);

        int seconds = 0;

        while (!await CheckConnection(connection))
        {
            Thread.Sleep(1000);

            seconds += 1000;

            if (seconds >= millisecondsTimeout) return false;
        }

        return true;
    }

    private static async Task<bool> CheckConnection(SqlConnection connection)
    {
        try
        {
            await connection.OpenAsync();
            return true;
        }
        catch (SqlException)
        {
            return false;
        }
    }
}
