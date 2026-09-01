using MinimalApiTemplate.Api.Common.Extensions;
using static MinimalApiTemplate.Api.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.CreateToDoItem;

public class CreateToDoItemEndpoint : IEndpoint 
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPostRoute("/todos", HandleAsync)
            .RequireAuthorization(Policies.StandardUser)
            .WithDescription("Used to create a todo")
            .WithTags(OpenApi.Tags.ToDos)
            .Produces(StatusCodes.Status400BadRequest);
    }

    public static async Task<CreatedAtRoute<CreateTodoItemResponse>> HandleAsync(
        [FromBody][Validate] CreateTodoItemRequest request,
        ISender sender, 
        IOutputCacheStore outputCacheStore,
        CancellationToken cancellationToken)
    {
        var command = request.MapToCommand();

        var newId = await sender.Send(command, cancellationToken);

        await outputCacheStore.EvictByTagAsync(OutputCacheTags.ToDoList, cancellationToken);

        return TypedResults.CreatedAtRoute(new CreateTodoItemResponse { Id = newId },
                                            "GetToDoItem", new { id = newId });
    }
}
