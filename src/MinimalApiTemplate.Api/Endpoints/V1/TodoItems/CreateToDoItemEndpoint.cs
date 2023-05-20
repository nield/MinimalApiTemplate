using Microsoft.AspNetCore.Mvc;
using MinimalApiTemplate.Api.Models.V1.Requests;
using MinimalApiTemplate.Api.Models.V1.Responses;
using MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems;

public class CreateToDoItemEndpoint : BaseEndpoint, 
    IEndpoint<IResult, CreateTodoItemRequest, CancellationToken>
{
    public CreateToDoItemEndpoint(ISender sender, IMapper mapper)
        : base(sender, mapper)
    {

    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.ToDoItemRoute()
            .MapPost("", ([FromBody][Validate] CreateTodoItemRequest request, CancellationToken cancellationToken) =>
                            HandleAsync(request, cancellationToken))
            .WithDescription("Used to create a todo")
            .Produces<CreateTodoItemResponse>(StatusCodes.Status201Created);
    }

    public async Task<IResult> HandleAsync(CreateTodoItemRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateTodoItemCommand>(request);

        var newId = await _mediator.Send(command, cancellationToken);

        return Results.CreatedAtRoute("GetToDoItem", new { id = newId },
                                    new CreateTodoItemResponse { Id = newId });
    }
}
