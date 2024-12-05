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
    private readonly IMapper _mapper;

    public UpdateTodoItemCommandHandler(
        IToDoItemRepository toDoItemRepository, 
        IMapper mapper)
    {
        _toDoItemRepository = toDoItemRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _toDoItemRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(TodoItem), request.Id);
        
        using (await AuditScope.CreateAsync("ToDoItem:Update", () => entity, cancellationToken: cancellationToken))
        {
            _mapper.Map(request, entity);

            await _toDoItemRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
