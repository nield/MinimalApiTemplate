using MinimalApiTemplate.Application.Common.Interfaces.Metrics;

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
    private readonly IMapper _mapper;
    private readonly IToDoItemMetrics _toDoItemMetrics;

    public CreateTodoItemCommandHandler(IToDoItemRepository toDoItemRepository, IMapper mapper, IToDoItemMetrics toDoItemMetrics)
    {
        _toDoItemRepository = toDoItemRepository;
        _mapper = mapper;
        _toDoItemMetrics = toDoItemMetrics;
    }

    public async Task<long> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TodoItem>(request);

        // Example adding event
        entity.AddDomainEvent(_mapper.Map<TodoItemCreatedEvent>(entity));

        using (AuditScope.Create("ToDoItem:Create", () => entity))
        {
            await _toDoItemRepository.AddAsync(entity, cancellationToken);
        }

        _toDoItemMetrics.ToDoItemsCreated(request.Title);

        return entity.Id;
    }
}
