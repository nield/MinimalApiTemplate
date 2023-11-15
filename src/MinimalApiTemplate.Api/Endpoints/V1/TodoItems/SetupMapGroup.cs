using static MinimalApiTemplate.Application.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems;

public static class SetupMapGroup
{
    public static RouteGroupBuilder ToDoItemRoute(this IEndpointRouteBuilder webApplication) =>
        webApplication
            .RouteV1()
            .MapGroup(OpenApi.Tags.ToDos)
            .WithTags(OpenApi.Tags.ToDos);
}
