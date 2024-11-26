using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetToDoItem;

public class GetToDoItemEndpoint : BaseEndpoint,
    IEndpoint<Ok<GetToDoItemResponse>, long, CancellationToken>
{
    public GetToDoItemEndpoint(ISender sender, IMapper mapper)
        : base(sender, mapper)
    {

    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.ToDoItemRouteV1()
            .MapGet("{id}", (long id, CancellationToken cancellationToken) =>
                HandleAsync(id, cancellationToken))
            .WithDescription("Used to get a single todo")
            .WithName("GetToDoItem")
            .Produces(StatusCodes.Status404NotFound);
    }

    public async Task<Ok<GetToDoItemResponse>> HandleAsync(long id, CancellationToken cancellationToken)
    {
        var query = new GetToDoItemQuery { Id = id };

        var data = await _mediator.Send(query, cancellationToken);

        var mappedData = _mapper.Map<GetToDoItemDto, GetToDoItemResponse>(data);

        return TypedResults.Ok(mappedData);
    }
}
