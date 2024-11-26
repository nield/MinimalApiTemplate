using MinimalApiTemplate.Infrastructure.Persistence;

namespace MinimalApiTemplate.Api.Configurations;

public static class HealthChecks
{
    public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration["RedisOptions:ConnectionString"]
            ?? throw new InvalidDataException("Redis connection string was not set");

        //MassTransit health check added in Infrastructure project 'ConfigureServices'
        services
            .AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>(name: "Database connectivity")
            .AddRedis(redisConnectionString, name: "Redis connectivity");
    }
}
