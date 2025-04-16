using Microsoft.Extensions.DependencyInjection;
using MinimalApiTemplate.Sdk.Handlers;
using MinimalApiTemplate.Sdk.Interfaces;
using MinimalApiTemplate.Sdk.Services;

namespace MinimalApiTemplate.Sdk;

public static class StartupExtensions
{
    public static void SetupMinimalApi(this IServiceCollection services, Uri baseAddress)
    {
        services.AddSingleton<ITokenService, TokenService>();

        services.AddTransient<AuthenticatedHttpClientHandler>();

        services.AddHttpClient<IMinimalApiTemplateV1Service, MinimalApiTemplateV1Service>(config =>
        {
            config.BaseAddress = baseAddress;
            config.Timeout = TimeSpan.FromSeconds(30);
        })
        .AddHeaderPropagation()
        .AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
        .AddStandardResilienceHandler(); 
    }
}
