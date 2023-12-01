using MinimalApiTemplate.Api.Models.V2.Responses;
using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Api.Endpoints.V2.TodoItems;

public class GetToDoItemEndpoint : BaseEndpoint, 
    IEndpoint<GetToDoItemResponse, long, CancellationToken>
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
            .Produces<GetToDoItemResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }

    public async Task<GetToDoItemResponse> HandleAsync(long id, CancellationToken cancellationToken)
    {
        var query = new GetToDoItemQuery { Id = id };

        var data = await _mediator.Send(query, cancellationToken);

        var mappedData = _mapper.Map<GetToDoItemDto, GetToDoItemResponse>(data);

        return mappedData;
    }
}
