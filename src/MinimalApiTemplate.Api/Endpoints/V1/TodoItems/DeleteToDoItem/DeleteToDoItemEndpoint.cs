using MinimalApiTemplate.Application.Features.TodoItems.Commands.DeleteTodoItem;
using static MinimalApiTemplate.Api.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.DeleteToDoItem;

public class DeleteToDoItemEndpoint : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.ToDoItemRouteV1()
            .MapDelete("{id}",
                ([FromRoute] long id,
                 ISender sender, 
                 IOutputCacheStore outputCacheStore,
                 CancellationToken cancellationToken) =>
                     HandleAsync(id, sender, outputCacheStore, cancellationToken))
            .WithDescription("Used to delete a todo")
            .Produces(StatusCodes.Status404NotFound);
    }

    public static async Task<NoContent> HandleAsync(
        long id, 
        ISender sender,
        IOutputCacheStore outputCacheStore,
        CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteTodoItemCommand(id), cancellationToken);

        await outputCacheStore.EvictByTagAsync(OutputCacheTags.ToDoList, cancellationToken);

        return TypedResults.NoContent();
    }
}
