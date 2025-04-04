using MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;
using static MinimalApiTemplate.Api.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.UpdateToDoItem;

public class UpdateToDoItemEndpoint : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.ToDoItemRouteV1()
            .MapPut("{id}",
                ([FromRoute] long id,
                [FromBody][Validate] UpdateTodoItemRequest request,
                ISender sender, 
                IOutputCacheStore outputCacheStore,
                CancellationToken cancellationToken) =>
                    HandleAsync(id, request, sender, outputCacheStore, cancellationToken))
            .RequireAuthorization(Policies.StandardUser)
            .WithDescription("Used to update a todo")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
    }

    public static async Task<NoContent> HandleAsync(
        long id, 
        UpdateTodoItemRequest request,
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
