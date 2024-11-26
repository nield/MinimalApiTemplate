namespace MinimalApiTemplate.Api.Common;

public static class MainRouteSetup
{
    public static RouteGroupBuilder UseMainRoute(this IEndpointRouteBuilder webApplication) =>
        webApplication.MapGroup("api/v{version:apiVersion}")
            .WithOpenApi()
            .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);
}
