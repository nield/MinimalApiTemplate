using MinimalApiTemplate.Worker.Configurations;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        var environment = hostContext.HostingEnvironment;

        services.AddWorkerInfrastructureServices(configuration, environment);

        services.SetupWorker(configuration);
    })
    .SetupLogging()
    .Build();

await host.RunAsync();
