using Microsoft.AspNetCore.Builder;
using MinimalApiTemplate.Worker.Configurations;

var builder = WebApplication.CreateSlimBuilder();

builder.AddServiceDefaults();

builder.ConfigureLogging();

builder.AddWorkerInfrastructureServices();

builder.SetupWorker();

var app = builder.Build();

app.MapDefaultEndpoints();

await app.RunAsync();