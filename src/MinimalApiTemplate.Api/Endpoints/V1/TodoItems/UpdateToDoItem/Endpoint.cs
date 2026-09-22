namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.UpdateToDoItem;

public class Endpoint : IEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPutRoute(ApiRoutes.Todos + "/{id}", HandleAsync)
            .RequireAuthorization(Policies.AdminOrStandardUser)
            .WithDescription("Used to update a todo")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(ApiTags.ToDos);
    }

    public static async Task<NoContent> HandleAsync(
        [FromRoute] long id, 
        [FromBody][Validate] Request request,
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
