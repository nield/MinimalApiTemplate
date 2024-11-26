using MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;
using static MinimalApiTemplate.Application.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.UpdateToDoItem;

public class UpdateToDoItemEndpoint : BaseEndpoint,
    IEndpoint<NoContent, long, UpdateTodoItemRequest, CancellationToken>
{
    private readonly IOutputCacheStore _outputCacheStore;

    public UpdateToDoItemEndpoint(ISender sender, IMapper mapper, IOutputCacheStore outputCacheStore)
        : base(sender, mapper)
    {
        _outputCacheStore = outputCacheStore;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.ToDoItemRouteV1()
            .MapPut("{id}",
                ([FromRoute] long id, [FromBody][Validate] UpdateTodoItemRequest request, CancellationToken cancellationToken) =>
                    HandleAsync(id, request, cancellationToken))
            .WithDescription("Used to update a todo")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
    }

    public async Task<NoContent> HandleAsync(long id, UpdateTodoItemRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateTodoItemCommand>(request);
        command.Id = id;

        await _mediator.Send(command, cancellationToken);

        await _outputCacheStore.EvictByTagAsync(OutputCacheTags.ToDoList, cancellationToken);

        return TypedResults.NoContent();
    }
}
