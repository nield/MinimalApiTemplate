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

    public static class MetricMeters
    {
        public const string GeneralMeter = "MinimalTemplate";
    }

    public static class Roles
    {
        public const string StandardUserRole = "standard-user-role";
        public const string AdminUserRole = "admin-user-role";
    }

    public static class Policies
    {
        public const string StandardUser = "StandardUser";
        public const string AdminUser = "AdminUser";
    }
}
