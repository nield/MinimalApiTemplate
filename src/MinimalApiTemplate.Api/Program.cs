using MinimalApi.Endpoint.Extensions;
using MinimalApiTemplate.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.ConfigureLogging();

// Add services to the container.
builder
    .AddApplicationServices()
    .AddInfrastructureServices()
    .AddApiServices();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseLogging();
app.UseHttpsRedirection();
app.UseCompression();

app.UseExceptionHandler(_ => { });

app.UseOutputCache();

app.UseHeaderPropagation();

app.MapPrometheusScrapingEndpoint("/metrics");

app.MapEndpoints();

if (!app.Environment.IsProduction())
{
    app.UseApiDocumentation();
}

await app.ApplyMigrations();

await app.RunAsync();

// Make the implicit Program class public so test projects can access it
public partial class Program
{
    protected Program()
    {

    }
}
