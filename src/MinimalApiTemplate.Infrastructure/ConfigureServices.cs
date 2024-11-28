using Audit.Core;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MinimalApiTemplate.Infrastructure.Common;
using MinimalApiTemplate.Infrastructure.Messaging;
using MinimalApiTemplate.Infrastructure.Persistence;
using MinimalApiTemplate.Infrastructure.Persistence.Interceptors;

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
        builder.Services.SetupMassTransit(configuration);
        builder.Services.SetupHttpClients(configuration);

        SetupAuditing(configuration);

        return builder;
    }

    public static IHostApplicationBuilder AddWorkerInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.SetupMetrics();
        builder.Services.SetupHttpClients(builder.Configuration);

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
        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        builder.Services.AddSingleton<ISaveChangesInterceptor, SoftDeleteSaveChangesInterceptor>();

        builder.Services.AddScoped<ApplicationDbContextInitialiser>();

        builder.Services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());        

        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(
                configuration.GetConnectionString("SqlDatabase"),
                builder =>
                    builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
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

    private static void SetupMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        MessageCorrelation.UseCorrelationId<BaseMessage>(x => Guid.Parse(x.CorrelationId));

        if (!IsMassTransitEnabled(configuration))
        {
            services.AddScoped<IPublishMessageService, MockPublishMessageService>();
            return;
        }
       
        services.AddScoped<IPublishMessageService, PublishMessageService>();

        services.AddMassTransit(config =>
        {
            config.AddTelemetryListener(true);

            config.ConfigureHealthCheckOptions(options => options.Name = "MassTransit connectivity");

            config.UsingRabbitMq((context, rabbitConfig) =>
            {
                var rabbitUri = configuration.GetConnectionString("RabbitMq");

                ArgumentException.ThrowIfNullOrWhiteSpace(rabbitUri);

                rabbitConfig.Host(new Uri(rabbitUri));
            });
        });                  
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
    private static bool IsMassTransitEnabled(IConfiguration configuration)
        => configuration.GetValue<bool>("MassTransit:PublishEnabled");
}
