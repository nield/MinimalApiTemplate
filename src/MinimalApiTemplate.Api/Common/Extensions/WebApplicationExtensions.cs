using System.Reflection;

namespace MinimalApiTemplate.Api.Common.Extensions;

public static class WebApplicationExtensions
{
    public static IApplicationBuilder MapEndpoints(
        this WebApplication app)
    {
        var endpoints = typeof(WebApplicationExtensions).Assembly
            .GetExportedTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false }
                        && t.IsAssignableTo(typeof(IEndpoint)));

        foreach (var type in endpoints)
        {
            AddRoute(type, app);
        }

        return app;
    }

    private static void AddRoute(Type type, IEndpointRouteBuilder app) =>
        typeof(WebApplicationExtensions)
            .GetMethod(nameof(AddRouteGeneric), BindingFlags.NonPublic | BindingFlags.Static)!
            .MakeGenericMethod(type)
            .Invoke(null, [app]);

    private static void AddRouteGeneric<TEndpoint>(IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint =>
        TEndpoint.AddRoute(app);
}
