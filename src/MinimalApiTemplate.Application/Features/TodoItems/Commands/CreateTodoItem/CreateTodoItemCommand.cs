namespace MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand : IRequest<long>
{
    public required string Title { get; set; }
    public string? Note { get; set; }
    public PriorityLevel Priority { get; set; }
    public DateTimeOffset? Reminder { get; set; }
    public List<string> Tags { get; set; } = [];
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, long>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IToDoItemMetrics _toDoItemMetrics;

    public CreateTodoItemCommandHandler(
        IToDoItemRepository toDoItemRepository, 
        IToDoItemMetrics toDoItemMetrics)
    {
        _toDoItemRepository = toDoItemRepository;
        _toDoItemMetrics = toDoItemMetrics;
    }

    public async Task<long> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = request.MapToEntity();

        // Example adding event
        entity.AddDomainEvent(entity.MapToEvent());

        using (await AuditScope.CreateAsync("ToDoItem:Create", () => entity, cancellationToken: cancellationToken))
        {
            await _toDoItemRepository.AddAsync(entity, cancellationToken);
        }

        _toDoItemMetrics.ToDoItemsCreated("created");

        return entity.Id;
    }
}
