using System.Diagnostics.CodeAnalysis;

namespace MinimalApiTemplate.Worker.Configurations;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static IHostApplicationBuilder SetupWorker(this IHostApplicationBuilder builder)
    {
        builder.SetupMassTransit();

        return builder;
    }
}

