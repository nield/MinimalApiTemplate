using System.Diagnostics.CodeAnalysis;
using MinimalApiTemplate.Application.Common.Interfaces;

namespace MinimalApiTemplate.Worker.Configurations;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static IHostApplicationBuilder SetupWorker(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
        
        builder.SetupMessaging();

        return builder;
    }
}

