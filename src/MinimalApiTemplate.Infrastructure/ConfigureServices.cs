using Audit.Core;
using MassTransit;
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
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        services.SetupDatabase(configuration, hostEnvironment);
        services.SetupCaching(configuration);
        services.SetupRepositories();
        services.SetupMetrics();
        services.SetupMassTransit(configuration);
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
            .AddHeaderPropagation()
            .AddStandardResilienceHandler();
    }

    private static void SetupDatabase(this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddSingleton<ISaveChangesInterceptor, SoftDeleteSaveChangesInterceptor>();

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                builder =>
                    builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                        .EnableRetryOnFailure(maxRetryCount: 3)
                        .MigrationsHistoryTable(ApplicationDbContext.MigrationTableName, ApplicationDbContext.DbSchema))
                    .EnableSensitiveDataLogging(hostEnvironment.IsDevelopment());
        });
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

            services.AddStackExchangeRedisOutputCache(options =>
            {
                options.Configuration = configuration["RedisOptions:ConnectionString"];
                options.InstanceName = configuration["RedisOptions:InstanceName"];
            });
        }
        else
        {
            services.AddDistributedMemoryCache();
        }

        services.AddOutputCache();
    }

    private static void SetupMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        if (!IsMassTransitEnabled(configuration))
        {
            services.AddScoped<IPublishMessageService, MockPublishMessageService>();
            return;
        }
        
        services.AddScoped<IPublishMessageService, PublishMessageService>();

        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((context, rabbitConfig) =>
            {
                var rabbitUri = configuration["MassTransit:RabbitMq:Uri"];

                ArgumentException.ThrowIfNullOrWhiteSpace(rabbitUri);

                rabbitConfig.Host(new Uri(rabbitUri));
            });
        });                  
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
    private static bool IsMassTransitEnabled(IConfiguration configuration)
        => configuration.GetValue<bool>("MassTransit:PublishEnabled");
}
