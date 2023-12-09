namespace MinimalApiTemplate.Application.Common;

public static class Constants
{
    public static class Environments
    {
        public const string Test = "Test";
    }

    public static class HttpClients
    {
        public const string DefaultClientName = "default";
    }

    public static class OpenApi
    {
        public static class Tags
        {
            public const string ToDos = "ToDos";
        }
    }

    public static class OutputCacheTags
    {
        public const string ToDoList = "ToDoList";
    }

    public static class MetricMeters
    {
        public const string GeneralMeter = "MinimalTemplate";
        public const string AspNetCoreHosting = "Microsoft.AspNetCore.Hosting";
        public const string AspNetCoreKestrel = "Microsoft.AspNetCore.Server.Kestrel";
    }

    public static class Headers
    {
        public const string CorrelationId = "x-correlation-id";
        public const string UserProfileId = "UserProfileId";
        public const string Authorization = "Authorization";
    }
}
