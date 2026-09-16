using MinimalApiTemplate.Application.Features.TodoItems.Commands.DeleteTodoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.DeleteToDoItem;

public class Endpoint : IEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDeleteRoute(ApiRoutes.Todos + "/{id}", HandleAsync)
            .WithDescription("Used to delete a todo")
            .RequireAuthorization(Policies.AdminUser)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(ApiTags.ToDos);
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
