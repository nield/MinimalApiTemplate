using static MinimalApiTemplate.Application.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V2;

public static class RoutesV2Setup
{
    public static RouteGroupBuilder UseRouteV2(this IEndpointRouteBuilder webApplication) =>
        webApplication
            .UseMainRoute()
            .WithApiVersionSet(VersionSets.GetVersionSet(2))
            .MapToApiVersion(2.0);

    public static RouteGroupBuilder ToDoItemRouteV2(this IEndpointRouteBuilder webApplication) =>
        webApplication
            .UseRouteV2()
            .MapGroup(OpenApi.Tags.ToDos)
            .WithTags(OpenApi.Tags.ToDos);
}
