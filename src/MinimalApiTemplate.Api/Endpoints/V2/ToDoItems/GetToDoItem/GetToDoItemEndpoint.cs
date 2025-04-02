using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;
using static MinimalApiTemplate.Api.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V2.ToDoItems.GetToDoItem;

public class GetToDoItemEndpoint : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.ToDoItemRouteV2()
            .MapGet("{id}",
                (long id,
                ISender sender, 
                IMapper mapper,
                CancellationToken cancellationToken) =>
                    HandleAsync(id, sender, mapper, cancellationToken))
            .RequireAuthorization(Policies.StandardUser)
            .WithDescription("Used to get a single todo")
            .WithName("GetToDoItemV2")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);
    }

    public static async Task<Ok<GetToDoItemResponse>> HandleAsync(
        long id,
        ISender sender,
        IMapper mapper,
        CancellationToken cancellationToken)
    {
        var query = new GetToDoItemQuery { Id = id };

        var data = await sender.Send(query, cancellationToken);

        var mappedData = mapper.Map<GetToDoItemDto, GetToDoItemResponse>(data);

        return TypedResults.Ok(mappedData);
    }
}
