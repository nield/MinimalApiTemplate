using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MinimalApi.Endpoint.Extensions;
using MinimalApiTemplate.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureLogging();

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment);
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

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

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/ping", () => "Working as expected")
    .ShortCircuit(200)
    .ExcludeFromDescription();

await app.ApplyMigrations();

await app.RunAsync();

// Make the implicit Program class public so test projects can access it
public partial class Program
{
    protected Program()
    {

    }
}
