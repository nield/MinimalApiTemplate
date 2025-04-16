namespace MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;

public record UpdateTodoItemCommand : IRequest
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public PriorityLevel Priority { get; set; }
    public string? Note { get; set; }
    public List<string> Tags { get; set; } = [];
}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly IToDoItemRepository _toDoItemRepository;

    public UpdateTodoItemCommandHandler(IToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }

    public async ValueTask<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _toDoItemRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(TodoItem), request.Id);
        
        using (await AuditScope.CreateAsync("ToDoItem:Update", () => entity, cancellationToken: cancellationToken))
        {
           request.MapToEntity(entity);

            await _toDoItemRepository.UpdateAsync(entity, cancellationToken);
        }

        return Unit.Value;
    }
}
