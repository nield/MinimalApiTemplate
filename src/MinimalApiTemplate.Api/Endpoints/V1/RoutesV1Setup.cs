using MinimalApiTemplate.Api.Common;
using static MinimalApiTemplate.Application.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1;

public static class RoutesV1Setup
{
    public static RouteGroupBuilder RouteV1(this IEndpointRouteBuilder webApplication) =>
        webApplication
            .UseMainRoute()
            .WithApiVersionSet(VersionSets.GetVersionSet(1))
            .MapToApiVersion(1.0);

    public static RouteGroupBuilder ToDoItemRouteV1(this IEndpointRouteBuilder webApplication) =>
        webApplication
            .RouteV1()
            .MapGroup(OpenApi.Tags.ToDos)
            .WithTags(OpenApi.Tags.ToDos);
}
