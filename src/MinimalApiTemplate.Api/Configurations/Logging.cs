using Serilog;

namespace MinimalApiTemplate.Api.Configurations;

public static class Logging
{
    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();

        builder.Host.UseSerilog((context, services, configuration)
                                    => configuration.ReadFrom.Configuration(context.Configuration));
    }

    public static void UseLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
    }
}
