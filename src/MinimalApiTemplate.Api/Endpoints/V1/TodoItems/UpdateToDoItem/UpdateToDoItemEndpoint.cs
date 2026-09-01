using MinimalApiTemplate.Api.Common.Extensions;
using static MinimalApiTemplate.Api.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.UpdateToDoItem;

public class UpdateToDoItemEndpoint : IEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPutRoute("/todos/{id}", HandleAsync)
            .RequireAuthorization(Policies.StandardUser)
            .WithDescription("Used to update a todo")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(OpenApi.Tags.ToDos);
    }

    public static async Task<NoContent> HandleAsync(
        [FromRoute] long id, 
        [FromBody][Validate] UpdateTodoItemRequest request,
        ISender sender,
        IOutputCacheStore outputCacheStore,
        CancellationToken cancellationToken)
    {
        var command = request.MapToCommand(id);

        await sender.Send(command, cancellationToken);

        await outputCacheStore.EvictByTagAsync(OutputCacheTags.ToDoList, cancellationToken);

        return TypedResults.NoContent();
    }
}
