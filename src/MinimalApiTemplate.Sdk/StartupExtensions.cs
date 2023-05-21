using Microsoft.Extensions.DependencyInjection;

namespace MinimalApiTemplate.Sdk;
public static class StartupExtensions
{
    public static void SetupMinimalApi(this IServiceCollection services, Uri baseAddress)
    {
        services.AddHttpClient<IMinimalApiTemplateV1Service, MinimalApiTemplateV1Service>("minimalApiClient", config =>
        {
            config.BaseAddress = baseAddress;
            config.Timeout = TimeSpan.FromSeconds(30);
        });
    }
}
