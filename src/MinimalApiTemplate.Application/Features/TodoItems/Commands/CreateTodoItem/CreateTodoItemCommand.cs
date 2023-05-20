namespace MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand : IRequest<long>
{
    public string Title { get; set; } = null!;
    public string? Note { get; set; }
    public PriorityLevel Priority { get; set; }
    public DateTimeOffset? Reminder { get; set; }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, long>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IMapper _mapper;

    public CreateTodoItemCommandHandler(IToDoItemRepository toDoItemRepository, IMapper mapper)
    {
        _toDoItemRepository = toDoItemRepository;
        _mapper = mapper;
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

        return entity.Id;
    }
}
