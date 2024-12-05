using MinimalApiTemplate.Application.Common.Interfaces.Metrics;
using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Worker.Consumers.V1.TodoItems;

public class ToDoItemCreatedConsumer : BaseConsumer<ToDoItemCreated>
{
    private readonly IToDoItemMetrics _toDoItemMetrics;

    public ToDoItemCreatedConsumer(IToDoItemMetrics toDoItemMetrics,
        ILogger<ToDoItemCreatedConsumer> logger)
        : base(logger)
    {
        _toDoItemMetrics = toDoItemMetrics;
    }

    protected override Task ProcessMessage(ConsumeContext<ToDoItemCreated> context)
    {
        _logger.LogInformation("Consumed message: {Message}", context.Message);

        _toDoItemMetrics.ToDoItemsCreatedEventProcessed("processed");

        return Task.CompletedTask;
    }
}
