using static MinimalApiTemplate.Api.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V2.ToDoItems;

public static class ToDoItemRouteSetup
{
    public static RouteGroupBuilder ToDoItemRouteV2(this IEndpointRouteBuilder webApplication) =>
        webApplication
            .UseRouteV2()
            .MapGroup(OpenApi.Tags.ToDos)
            .WithTags(OpenApi.Tags.ToDos);
}
