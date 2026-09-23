using Audit.Core;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MinimalApiTemplate.Infrastructure.Common;
using MinimalApiTemplate.Infrastructure.Messaging;
using MinimalApiTemplate.Infrastructure.Persistence;
using MinimalApiTemplate.Infrastructure.Persistence.Interceptors;
using Rebus.Config;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var hostEnvironment = builder.Environment;

        builder.SetupDatabase(configuration, hostEnvironment);
        builder.SetupCaching();
        builder.Services.SetupRepositories();
        builder.Services.SetupMetrics();
        builder.SetupMessaging();
        builder.Services.SetupHttpClients(configuration);

        SetupAuditing(configuration);

        SetThreadPoolMinThreads(500);

        return builder;
    }

    public static IHostApplicationBuilder AddWorkerInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var hostEnvironment = builder.Environment;
        
        builder.SetupDatabase(configuration, hostEnvironment);
        builder.SetupCaching();
        builder.Services.SetupRepositories();
        builder.Services.SetupMetrics();
        builder.Services.SetupHttpClients(builder.Configuration);

        SetupAuditing(configuration);
        
        SetThreadPoolMinThreads(500);

        return builder;
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
            .AddHeaderPropagation();
    }

    private static void SetupDatabase(this IHostApplicationBuilder builder,
        IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        builder.Services.AddSingleton<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();
        builder.Services.AddSingleton<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        builder.Services.AddSingleton<ISaveChangesInterceptor, SoftDeleteSaveChangesInterceptor>();

        builder.Services.AddScoped<ApplicationDbContextInitialiser>();

        builder.Services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());        

        builder.Services.AddDbContextPool<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(
                configuration.GetConnectionString("SqlDatabase"),
                sqlBuilder =>
                    sqlBuilder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                        .EnableRetryOnFailure(maxRetryCount: 3)
                        .MigrationsHistoryTable(ApplicationDbContext.MigrationTableName, ApplicationDbContext.DbSchema))
                    .EnableSensitiveDataLogging(hostEnvironment.IsDevelopment());
        });

        builder.EnrichSqlServerDbContext<ApplicationDbContext>();
    }

    private static void SetupRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblyOf<IInfrastructureMarker>()
                                    .AddClasses(c => c.AssignableTo(typeof(IRepository<>)))
                                    .AsImplementedInterfaces()
                                    .WithScopedLifetime());
    }

    private static void SetupMetrics(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblyOf<IInfrastructureMarker>()
                                    .AddClasses(c => c.AssignableTo<IMetric>())
                                    .AsImplementedInterfaces()
                                    .WithSingletonLifetime());
    }

    private static void SetupCaching(this IHostApplicationBuilder builder)
    {
        builder.AddRedisDistributedCache("Redis");
        builder.AddRedisOutputCache("Redis");
    }

    private static void SetupMessaging(this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        if (!configuration.GetValue<bool>("Messaging:PublishEnabled"))
        {
            builder.Services.AddScoped<IPublishMessageService, MockPublishMessageService>();
            return;
        }

        builder.Services.AddScoped<IPublishMessageService, PublishMessageService>();

        var rabbitUri = configuration.GetConnectionString("RabbitMq");

        ArgumentException.ThrowIfNullOrWhiteSpace(rabbitUri);

        builder.Services.AddRebus(configure => configure
            .Transport(t => t.UseRabbitMqAsOneWayClient(rabbitUri))
            .Options(o => o.EnableDiagnosticSources()));

        builder.AddRabbitMQClient(connectionName: "RabbitMq");
    }

    private static void SetupAuditing(IConfiguration configuration)
    {
        Audit.Core.Configuration.Setup()
            .UseSqlServer(config => config
                .ConnectionString(configuration.GetConnectionString("SqlDatabase"))
                .Schema(ApplicationDbContext.DbSchema)
                .TableName("Event")
                .IdColumnName("EventId")
                .JsonColumnName("JsonData")
                .LastUpdatedColumnName("LastUpdatedDate")
                .CustomColumn("EventType", ev => ev.EventType));
    }

    private static void SetThreadPoolMinThreads(int minThreadCount) =>
        ThreadPool.SetMinThreads(minThreadCount, minThreadCount);
}
