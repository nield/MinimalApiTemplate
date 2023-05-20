namespace MinimalApiTemplate.Api.Endpoints.V1;

public static class MapGroupsV1Setup
{
    public static RouteGroupBuilder RouteV1(this IEndpointRouteBuilder webApplication) =>
        webApplication
            .Route()
            .WithApiVersionSet(VersionsSets.GetVersionSet(1))
            .MapToApiVersion(1.0);
}
