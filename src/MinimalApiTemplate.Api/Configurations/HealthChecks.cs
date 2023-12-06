using MinimalApiTemplate.Infrastructure.Persistence;

namespace MinimalApiTemplate.Api.Configurations;

public static class HealthChecks
{
    public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration["RedisOptions:ConnectionString"]
            ?? throw new InvalidDataException("Redis connection string was not set");

        services
            .AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>("Database connectivity")
            .AddRedis(redisConnectionString, "Redis Connectivity");
    }
}
