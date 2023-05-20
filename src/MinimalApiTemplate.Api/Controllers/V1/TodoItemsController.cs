//using MinimalApiTemplate.Api.Models.V1.Requests;
//using MinimalApiTemplate.Api.Models.V1.Responses;
//using MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;
//using MinimalApiTemplate.Application.Features.TodoItems.Commands.DeleteTodoItem;
//using MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;
//using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;
//using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;
//using Microsoft.AspNetCore.Mvc;

//namespace MinimalApiTemplate.Api.Controllers.V1;

//[ApiVersion("1.0")]
//[ProducesResponseType(StatusCodes.Status400BadRequest)]
//public class TodoItemsController : ApiControllerBase
//{
//    public TodoItemsController(ISender sender, IMapper mapper)
//        : base(sender, mapper)
//    {

//    }

//    [HttpGet("{id}", Name = "GetToDoItem")]
//    [ProducesResponseType(typeof(GetToDoItemResponse), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<ActionResult<GetToDoItemResponse>> Get(long id, CancellationToken cancellationToken)
//    {
//        var query = new GetToDoItemQuery { Id = id };

//        var data = await _mediator.Send(query, cancellationToken);

//        var mappedData = _mapper.Map<GetToDoItemDto, GetToDoItemResponse>(data);

//        return mappedData;
//    }

//    [HttpPost]
//    [ProducesResponseType(typeof(CreateTodoItemResponse), StatusCodes.Status201Created)]
//    public async Task<ActionResult> Create(CreateTodoItemRequest request, CancellationToken cancellationToken)
//    {
//        var command = _mapper.Map<CreateTodoItemCommand>(request);

//        var newId = await _mediator.Send(command, cancellationToken);

//        return CreatedAtRoute("GetToDoItem", new { id = newId },
//                                new CreateTodoItemResponse { Id = newId });
//    }

//    [HttpPut("{id}")]
//    [ProducesResponseType(StatusCodes.Status204NoContent)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<ActionResult> Update(long id, UpdateTodoItemRequest request, CancellationToken cancellationToken)
//    {
//        var command = _mapper.Map<UpdateTodoItemCommand>(request);
//        command.Id = id;

//        await _mediator.Send(command, cancellationToken);

//        return NoContent();
//    }

//    [HttpDelete("{id}")]
//    [ProducesResponseType(StatusCodes.Status204NoContent)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
//    {
//        await _mediator.Send(new DeleteTodoItemCommand(id), cancellationToken);

//        return NoContent();
//    }

//    [HttpGet]
//    public async Task<ActionResult<PaginatedListResponse<GetToDoItemsResponse>>> GetTodoItemsWithPagination([FromQuery] GetTodoItemsWithPaginationRequest request, CancellationToken cancellationToken)
//    {
//        var query = _mapper.Map<GetTodoItemsWithPaginationQuery>(request);

//        var data = await _mediator.Send(query, cancellationToken);

//        var mappedData = _mapper.Map<PaginatedListResponse<GetToDoItemsResponse>>(data);

//        return mappedData;        
//    }
//}
