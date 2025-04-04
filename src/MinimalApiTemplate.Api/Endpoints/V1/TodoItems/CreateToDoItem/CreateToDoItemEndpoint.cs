using static MinimalApiTemplate.Api.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.CreateToDoItem;

public class CreateToDoItemEndpoint : IEndpoint 
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.ToDoItemRouteV1()
            .MapPost("", 
                ([FromBody][Validate] CreateTodoItemRequest request,
                ISender sender,
                IOutputCacheStore outputCacheStore,
                CancellationToken cancellationToken) =>
                    HandleAsync(request, sender, outputCacheStore, cancellationToken))
            .RequireAuthorization(Policies.StandardUser)
            .WithDescription("Used to create a todo")
            .Produces(StatusCodes.Status400BadRequest);
    }

    public static async Task<CreatedAtRoute<CreateTodoItemResponse>> HandleAsync(
        CreateTodoItemRequest request,
        ISender sender, 
        IOutputCacheStore outputCacheStore,
        CancellationToken cancellationToken)
    {
        var command = request.MapCreateTodoItemCommand();

        var newId = await sender.Send(command, cancellationToken);

        await outputCacheStore.EvictByTagAsync(OutputCacheTags.ToDoList, cancellationToken);

        return TypedResults.CreatedAtRoute(new CreateTodoItemResponse { Id = newId },
                                            "GetToDoItem", new { id = newId });
    }
}
