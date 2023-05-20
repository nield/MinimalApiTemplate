using MinimalApiTemplate.Api.Integration.Tests.Containers;

namespace MinimalApiTemplate.Api.Integration.Tests;

internal static class Environment
{
    internal static string DatabaseConnectionString => DatabaseContainer.Instance.GetDatabaseConnectionString();
    internal static string CacheConnectionString => CacheContainer.Instance.GetCacheConnectionString();
    internal static string RabbitConnectionString = RabbitContainer.Instance.GetRabbitConnectionString();
}