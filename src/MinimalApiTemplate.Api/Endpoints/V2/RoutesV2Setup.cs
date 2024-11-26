namespace MinimalApiTemplate.Api.Endpoints.V2;

public static class RoutesV2Setup
{
    public static RouteGroupBuilder UseRouteV2(this IEndpointRouteBuilder webApplication) =>
        webApplication
            .UseMainRoute()
            .WithApiVersionSet(VersionSets.GetVersionSet(2))
            .MapToApiVersion(2.0);
}
