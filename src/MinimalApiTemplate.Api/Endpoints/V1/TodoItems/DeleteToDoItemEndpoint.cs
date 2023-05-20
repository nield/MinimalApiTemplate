using Microsoft.AspNetCore.Mvc;
using MinimalApiTemplate.Api.Models.V1.Responses;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.DeleteTodoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems;

public class DeleteToDoItemEndpoint : BaseEndpoint, 
    IEndpoint<IResult, long, CancellationToken>
{
    public DeleteToDoItemEndpoint(ISender sender, IMapper mapper)
        :base(sender, mapper)
    {

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

        return Results.NoContent();
    }
}
