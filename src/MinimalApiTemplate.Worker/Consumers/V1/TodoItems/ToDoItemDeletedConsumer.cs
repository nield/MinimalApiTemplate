using MinimalApiTemplate.Application.Common.Interfaces.Metrics;
using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Worker.Consumers.V1.TodoItems;

public class ToDoItemDeletedConsumer : BaseConsumer<ToDoItemDeleted>
{
    private readonly IToDoItemMetrics _toDoItemMetrics;

    public ToDoItemDeletedConsumer(IToDoItemMetrics toDoItemMetrics,
        ILogger<ToDoItemCreatedConsumer> logger)
        : base(logger)
    {
        _toDoItemMetrics = toDoItemMetrics;
    }

    protected override Task ProcessMessage(ToDoItemDeleted message)
    {
        _logger.LogInformation("Consumed message: {Message}", message);

        _toDoItemMetrics.ToDoItemsDeletedEventProcessed("processed");

        return Task.CompletedTask;
    }
}