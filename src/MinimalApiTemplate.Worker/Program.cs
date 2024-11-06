using MinimalApiTemplate.Worker.Configurations;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        services.AddWorkerInfrastructureServices(configuration);

        services.SetupWorker(configuration);
    })
    .SetupLogging()
    .Build();

await host.RunAsync();
