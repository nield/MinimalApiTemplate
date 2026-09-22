namespace MinimalApiTemplate.Application.Features.TodoItems.Commands.DeleteTodoItem;

public record DeleteTodoItemCommand(long Id) : IRequest;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IToDoItemMetrics _toDoItemMetrics;

    public DeleteTodoItemCommandHandler(
        IToDoItemRepository toDoItemRepository,
        IToDoItemMetrics toDoItemMetrics)
    {
        _toDoItemRepository = toDoItemRepository;
        _toDoItemMetrics = toDoItemMetrics;
    }

    public async ValueTask<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _toDoItemRepository.GetByIdAsync(request.Id, cancellationToken) 
            ?? throw new NotFoundException(nameof(TodoItem), request.Id);
        
        using (await AuditScope.CreateAsync("ToDoItem:Delete", () => entity, cancellationToken: cancellationToken))
        {
            // Example adding event
            entity.AddDomainEvent(new TodoItemDeletedEvent { Id = request.Id });

            await _toDoItemRepository.DeleteAsync(entity, cancellationToken);
        }

        _toDoItemMetrics.ToDoItemsDeleted("deleted");
        
        return Unit.Value;
    }
}
