using MinimalApiTemplate.Infrastructure.Persistence;

namespace MinimalApiTemplate.Api.Configurations;

public static class HealthChecks
{
    public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>("Database connectivity")
            .AddRedis(configuration["RedisOptions:ConnectionString"], "Redis Connectivity");
    }
}
