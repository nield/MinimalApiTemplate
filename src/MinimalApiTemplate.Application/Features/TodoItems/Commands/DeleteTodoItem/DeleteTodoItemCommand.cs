namespace MinimalApiTemplate.Application.Features.TodoItems.Commands.DeleteTodoItem;

public record DeleteTodoItemCommand(long Id) : IRequest;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly IToDoItemRepository _toDoItemRepository;

    public DeleteTodoItemCommandHandler(IToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
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

        return Unit.Value;
    }
}
