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

    public override async Task StartContainerAsync()
    {
        await base.StartContainerAsync();

        var seconds = 0;

        while (!await IsServerConnectedAsync())
        {
            Thread.Sleep(1000);

            seconds += 1000;

            if (seconds >= 100000)
            {
                throw new OperationCanceledException("The containers did not start in time");
            }
        }
    }

    private async Task<bool> IsServerConnectedAsync()
    {
        var dbConnectionString = GetConnectionString();

        var dbMasterConnectionString = dbConnectionString.Replace(DatabaseName, "master");

        using var connection = new SqlConnection(dbMasterConnectionString);

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
