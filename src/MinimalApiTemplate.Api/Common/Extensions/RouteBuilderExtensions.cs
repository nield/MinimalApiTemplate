namespace MinimalApiTemplate.Api.Common.Extensions;

public static class RouteBuilderExtensions
{
    public static RouteGroupBuilder UseMainRoute(this IEndpointRouteBuilder webApplication, int majorVersion = 1) =>
        webApplication.MapGroup("api/v{version:apiVersion}")            
            .WithApiVersionSet(VersionSets.GetVersionSet(majorVersion))
            .MapToApiVersion(majorVersion);

    public static RouteHandlerBuilder MapGetRoute(this IEndpointRouteBuilder webApplication,
        string pattern, Delegate handler, int majorVersion = 1) =>
            webApplication
                .UseMainRoute(majorVersion)
                .MapGet(pattern, handler);

    public static RouteHandlerBuilder MapPostRoute(this IEndpointRouteBuilder webApplication,
        string pattern, Delegate handler, int majorVersion = 1) =>
            webApplication
                .UseMainRoute(majorVersion)
                .MapPost(pattern, handler)
                .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);

    public static RouteHandlerBuilder MapPutRoute(this IEndpointRouteBuilder webApplication,
        string pattern, Delegate handler, int majorVersion = 1) =>
            webApplication
                .UseMainRoute(majorVersion)
                .MapPut(pattern, handler)
                .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);

    public static RouteHandlerBuilder MapDeleteRoute(this IEndpointRouteBuilder webApplication,
        string pattern, Delegate handler, int majorVersion = 1) =>
            webApplication
                .UseMainRoute(majorVersion)
                .MapDelete(pattern, handler)
                .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);
}
