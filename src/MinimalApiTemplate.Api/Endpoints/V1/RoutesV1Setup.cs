namespace MinimalApiTemplate.Api.Endpoints.V1;

public static class RoutesV1Setup
{
    public static RouteGroupBuilder RouteV1(this IEndpointRouteBuilder webApplication) =>
        webApplication
            .UseMainRoute()
            .WithApiVersionSet(VersionSets.GetVersionSet(1))
            .MapToApiVersion(1.0);
}
