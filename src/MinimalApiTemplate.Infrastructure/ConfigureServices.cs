using Audit.Core;
using MinimalApiTemplate.Infrastructure.Common;
using MinimalApiTemplate.Infrastructure.Persistence;
using MinimalApiTemplate.Infrastructure.Persistence.Interceptors;
using Polly.Extensions.Http;
using Polly;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Polly.Retry;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
        IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        services.SetupDatabase(configuration);
        services.SetupCaching(configuration);
        services.SetupRepositories();
        services.SetupAuditing(configuration);
        services.SetupHttpClients(configuration);

        return services;
    }

    private static void SetupHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpClient(
                Constants.HttpClients.DefaultClientName,
                (config) =>
                {
                    var baseUri = configuration["ExternalServices:Default:BaseUrl"]
                        ?? throw new InvalidDataException("External services base url was not set");

                    config.BaseAddress = new Uri(baseUri);
                    config.Timeout = TimeSpan.FromSeconds(30);
                }
            )
            .AddPolicyHandler(GetRetryPolicy());
    }

    private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TaskCanceledException>()
            .Or<TimeoutException>()
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    private static void SetupDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                                    .EnableRetryOnFailure(maxRetryCount: 3)
                                    .MigrationsHistoryTable("__EFMigrationsHistory", ApplicationDbContext.DbSchema)));       
    }

    private static void SetupRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblyOf<IInfrastructureMarker>()
                                    .AddClasses(c => c.AssignableTo(typeof(IRepository<>)))
                                    .AsImplementedInterfaces()
                                    .WithScopedLifetime());

    }

    private static void SetupCaching(this IServiceCollection services, 
        IConfiguration configuration)
    {
        if (!string.IsNullOrEmpty(configuration["RedisOptions:ConnectionString"]))
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["RedisOptions:ConnectionString"];
                options.InstanceName = configuration["RedisOptions:InstanceName"];
            });
        }
        else
        {
            services.AddDistributedMemoryCache();
        }
    }

    private static void SetupAuditing(this IServiceCollection services, IConfiguration configuration)
    {
        Audit.Core.Configuration.Setup()
            .UseSqlServer(config => config
                .ConnectionString(configuration.GetConnectionString("DefaultConnection"))
                .Schema(ApplicationDbContext.DbSchema)
                .TableName("Event")
                .IdColumnName("EventId")
                .JsonColumnName("JsonData")
                .LastUpdatedColumnName("LastUpdatedDate")
                .CustomColumn("EventType", ev => ev.EventType));
    }
}
