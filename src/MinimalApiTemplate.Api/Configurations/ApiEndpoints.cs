namespace MinimalApiTemplate.Api.Configurations;

public static class ApiEndpoints
{
    public static IServiceCollection AddApiEndpoints(this IServiceCollection services)
    {
        var endpoints = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetExportedTypes())
            .Where(t => t.GetInterfaces().Contains(typeof(IEndpoint)))
            .Where(t => !t.IsInterface);

        foreach (var endpoint in endpoints)
        {
            services.AddTransient(typeof(IEndpoint), endpoint);
        }

        return services;
    }
}
