using MinimalApiTemplate.Api.Models.V2.Responses;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Api.Endpoints.V2.TodoItems;

public class GetToDoItemEndpoint : BaseEndpoint, 
    IEndpoint<Ok<GetToDoItemResponse>, long, CancellationToken>
{
    public GetToDoItemEndpoint(ISender sender, IMapper mapper)
        : base(sender, mapper)
    {

    }

    public void AddRoute(IEndpointRouteBuilder app)
    { 
        app.ToDoItemRouteV2()
            .MapGet("{id}", (long id, CancellationToken cancellationToken) =>
                HandleAsync(id, cancellationToken))
            .WithDescription("Used to get a single todo")
            .WithName("GetToDoItemV2")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);
    }

    public async Task<Ok<GetToDoItemResponse>> HandleAsync(long id, CancellationToken cancellationToken)
    {
        var query = new GetToDoItemQuery { Id = id };

        var data = await _mediator.Send(query, cancellationToken);

        var mappedData = _mapper.Map<GetToDoItemDto, GetToDoItemResponse>(data);

        return TypedResults.Ok(mappedData);
    }
}
