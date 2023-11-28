using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MinimalApiTemplate.Api.Models.V1.Responses;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.DeleteTodoItem;
using static MinimalApiTemplate.Application.Common.Constants;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems;

public class DeleteToDoItemEndpoint : BaseEndpoint, 
    IEndpoint<IResult, long, CancellationToken>
{
    private readonly IOutputCacheStore _outputCacheStore;

    public DeleteToDoItemEndpoint(ISender sender, IMapper mapper, IOutputCacheStore outputCacheStore)
        :base(sender, mapper)
    {
        _outputCacheStore = outputCacheStore;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.ToDoItemRoute()
            .MapDelete("{id}",
                 ([FromRoute] long id, CancellationToken cancellationToken) =>
                     HandleAsync(id, cancellationToken))
            .WithDescription("Used to delete a todo")
            .Produces<CreateTodoItemResponse>(StatusCodes.Status204NoContent);
    }

    public async Task<IResult> HandleAsync(long id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteTodoItemCommand(id), cancellationToken);

        await _outputCacheStore.EvictByTagAsync(OutputCacheTags.ToDoList, cancellationToken);

        return Results.NoContent();
    }
}
