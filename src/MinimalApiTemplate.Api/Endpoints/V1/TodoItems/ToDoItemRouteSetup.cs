using static MinimalApiTemplate.Api.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems;

public static class ToDoItemRouteSetup
{
    public static RouteGroupBuilder ToDoItemRouteV1(this IEndpointRouteBuilder webApplication) =>
        webApplication
            .RouteV1()
            .MapGroup(OpenApi.Tags.ToDos)
            .WithTags(OpenApi.Tags.ToDos);
}
