using Serilog;

namespace MinimalApiTemplate.Worker.Configurations;

public static class Logging
{
    public static IHostBuilder SetupLogging(this IHostBuilder builder)
    {
        return builder.UseSerilog((context, services, configuration)
                                    => configuration.ReadFrom.Configuration(context.Configuration));
    }
}
