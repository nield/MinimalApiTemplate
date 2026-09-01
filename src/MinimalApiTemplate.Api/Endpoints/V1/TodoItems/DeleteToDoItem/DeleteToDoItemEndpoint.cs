using MinimalApiTemplate.Api.Common.Extensions;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.DeleteTodoItem;
using static MinimalApiTemplate.Api.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.DeleteToDoItem;

public class DeleteToDoItemEndpoint : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDeleteRoute("/todos/{id}", HandleAsync)
            .WithDescription("Used to delete a todo")
            .RequireAuthorization(Policies.AdminUser)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(OpenApi.Tags.ToDos);
    }

    public static async Task<NoContent> HandleAsync(
        [FromRoute] long id, 
        ISender sender,
        IOutputCacheStore outputCacheStore,
        CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteTodoItemCommand(id), cancellationToken);

        await outputCacheStore.EvictByTagAsync(OutputCacheTags.ToDoList, cancellationToken);

        return TypedResults.NoContent();
    }
}
