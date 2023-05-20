namespace MinimalApiTemplate.Api.Endpoints.V1;

public static class MapGroupsV2Setup
{
    public static RouteGroupBuilder RouteV2(this IEndpointRouteBuilder webApplication) =>
        webApplication
            .Route()
            .WithApiVersionSet(VersionsSets.GetVersionSet(2))
            .MapToApiVersion(2.0);
}
