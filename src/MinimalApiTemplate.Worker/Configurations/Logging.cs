using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace MinimalApiTemplate.Worker.Configurations;

[ExcludeFromCodeCoverage]
public static class Logging
{
    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, services, configuration)
                                    => configuration.ReadFrom.Configuration(context.Configuration),
                                writeToProviders: true);
    }
}