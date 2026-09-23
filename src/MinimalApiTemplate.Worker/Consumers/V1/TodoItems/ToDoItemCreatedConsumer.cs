using MinimalApiTemplate.Application.Common.Interfaces.Metrics;
using MinimalApiTemplate.Application.Common.Interfaces.Repositories;
using MinimalApiTemplate.Domain.Enums;
using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Worker.Consumers.V1.TodoItems;

public class ToDoItemCreatedConsumer : BaseConsumer<ToDoItemCreated>
{
    private readonly IToDoItemMetrics _toDoItemMetrics;
    private readonly IToDoItemRepository _repository;

    public ToDoItemCreatedConsumer(IToDoItemMetrics toDoItemMetrics,
        IToDoItemRepository repository,
        ILogger<ToDoItemCreatedConsumer> logger)
        : base(logger)
    {
        _toDoItemMetrics = toDoItemMetrics;
        _repository = repository;
    }

    protected override async Task ProcessMessage(ToDoItemCreated message)
    {
        // Using below just to test db access from worker
        var todo = await _repository.GetByIdAsync(message.Id);

        if (todo is { Priority: PriorityLevel.None })
        {
            todo.Priority = PriorityLevel.Low;
            
            await _repository.UpdateAsync(todo);
        }
        
        _logger.LogInformation("Consumed message: {Message}", message);
        
        _toDoItemMetrics.ToDoItemsCreatedEventProcessed("processed");
    }
}
