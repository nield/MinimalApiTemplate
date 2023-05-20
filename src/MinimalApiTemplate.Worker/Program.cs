using MinimalApiTemplate.Worker.Configurations;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        services.SetupServices(configuration);
    })
    .SetupLogging()
    .Build();

await host.RunAsync();


