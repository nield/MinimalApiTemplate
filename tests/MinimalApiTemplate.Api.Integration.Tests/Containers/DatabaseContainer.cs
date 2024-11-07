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
           .WithReuse(true)
           .Build();
    }

    public override string GetConnectionString() =>
        $"Server={_container!.Hostname},{_container.GetMappedPublicPort(DatabaseDefaultPort)};Database={DatabaseName};User Id={DatabaseUsername};Password={DatabasePassword};TrustServerCertificate=True";

    public override async Task StartContainerAsync(CancellationToken cancellationToken)
    {
        await base.StartContainerAsync(cancellationToken);

        if (!await IsServerConnectedAsync(cancellationToken))
        {
            throw new OperationCanceledException("The container connection was not open in time");
        }
    }

    private async Task<bool> IsServerConnectedAsync(CancellationToken cancellationToken)
    {
        var dbConnectionString = GetConnectionString();

        var dbMasterConnectionString = dbConnectionString.Replace(DatabaseName, "master");

        using var connection = new SqlConnection(dbMasterConnectionString);

        while (!cancellationToken.IsCancellationRequested)
        {
            var connected = await CheckConnection(connection, cancellationToken);

            if (connected) return true;

            Thread.Sleep(250);
        }

        return false;
    }

    private static async Task<bool> CheckConnection(
        SqlConnection connection, 
        CancellationToken cancellationToken)
    {
        try
        {
            await connection.OpenAsync(cancellationToken);

            return true;
        }
        catch (SqlException)
        {
            return false;
        }
    }
}
