using MinimalApiTemplate.Api.Models.V1.Requests;
using MinimalApiTemplate.Api.Models.V1.Responses;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;
using static MinimalApiTemplate.Application.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems;

public class CreateToDoItemEndpoint : BaseEndpoint,
    IEndpoint<CreatedAtRoute<CreateTodoItemResponse>, CreateTodoItemRequest, CancellationToken>
{
    private readonly IOutputCacheStore _outputCacheStore;

    public CreateToDoItemEndpoint(ISender sender, IMapper mapper, IOutputCacheStore outputCacheStore)
        : base(sender, mapper)
    {
        _outputCacheStore = outputCacheStore;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.ToDoItemRouteV1()
            .MapPost("", ([FromBody][Validate] CreateTodoItemRequest request, CancellationToken cancellationToken) =>
                HandleAsync(request, cancellationToken))
            .WithDescription("Used to create a todo")
            .Produces(StatusCodes.Status400BadRequest);
    }

    public async Task<CreatedAtRoute<CreateTodoItemResponse>> HandleAsync(CreateTodoItemRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateTodoItemCommand>(request);

        var newId = await _mediator.Send(command, cancellationToken);

        await _outputCacheStore.EvictByTagAsync(OutputCacheTags.ToDoList, cancellationToken);

        return TypedResults.CreatedAtRoute(new CreateTodoItemResponse { Id = newId },
                                            "GetToDoItem", new { id = newId });
    }
}
