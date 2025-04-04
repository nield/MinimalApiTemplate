using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetToDoItem;

public class GetToDoItemEndpoint : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.ToDoItemRouteV1()
            .MapGet("{id}",
                (long id,
                ISender sender, 
                CancellationToken cancellationToken) =>
                    HandleAsync(id, sender, cancellationToken))
            .RequireAuthorization(Policies.StandardUser)
            .WithDescription("Used to get a single todo")
            .WithName("GetToDoItem")
            .Produces(StatusCodes.Status404NotFound);
    }

    public static async Task<Ok<GetToDoItemResponse>> HandleAsync(
        long id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetToDoItemQuery { Id = id };

        var data = await sender.Send(query, cancellationToken);

        var mappedData = data.MapGetToDoItemResponse();

        return TypedResults.Ok(mappedData);
    }
}
