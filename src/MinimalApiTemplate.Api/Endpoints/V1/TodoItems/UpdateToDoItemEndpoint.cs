using Microsoft.AspNetCore.Mvc;
using MinimalApiTemplate.Api.Models.V1.Requests;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems;

public class UpdateToDoItemEndpoint : BaseEndpoint, 
    IEndpoint<IResult, long, UpdateTodoItemRequest, CancellationToken>
{
    public UpdateToDoItemEndpoint(ISender sender, IMapper mapper)
        : base(sender, mapper)
    {

    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.ToDoItemRoute()
            .MapPut("{id}",
                ([FromRoute] long id, [FromBody][Validate] UpdateTodoItemRequest request, CancellationToken cancellationToken) =>
                    HandleAsync(id, request, cancellationToken))
            .WithDescription("Used to update a todo")
            .Produces(StatusCodes.Status204NoContent);
    }

    public async Task<IResult> HandleAsync(long id, UpdateTodoItemRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateTodoItemCommand>(request);
        command.Id = id;

        await _mediator.Send(command, cancellationToken);

        return Results.NoContent();
    }
}
