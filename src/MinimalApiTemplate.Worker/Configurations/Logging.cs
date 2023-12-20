using System.Diagnostics.CodeAnalysis;
using Serilog;

namespace MinimalApiTemplate.Worker.Configurations;

[ExcludeFromCodeCoverage]
public static class Logging
{
    public static IHostBuilder SetupLogging(this IHostBuilder builder)
    {
        return builder.UseSerilog((context, services, configuration)
                                    => configuration.ReadFrom.Configuration(context.Configuration));
    }
}