namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.CreateToDoItem;

public class Endpoint : IEndpoint 
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPostRoute(ApiRoutes.Todos, HandleAsync)
            .RequireAuthorization(Policies.AdminOrStandardUser)
            .WithDescription("Used to create a todo")
            .WithTags(ApiTags.ToDos)
            .Produces(StatusCodes.Status400BadRequest);
    }

    public static async Task<CreatedAtRoute<Response>> HandleAsync(
        [FromBody][Validate] Request request,
        ISender sender, 
        IOutputCacheStore outputCacheStore,
        CancellationToken cancellationToken)
    {
        var command = request.MapToCommand();

        var newId = await sender.Send(command, cancellationToken);

        await outputCacheStore.EvictByTagAsync(OutputCacheTags.ToDoList, cancellationToken);

        return TypedResults.CreatedAtRoute(new Response { Id = newId },
                                            "GetToDoItem", new { id = newId });
    }
}
