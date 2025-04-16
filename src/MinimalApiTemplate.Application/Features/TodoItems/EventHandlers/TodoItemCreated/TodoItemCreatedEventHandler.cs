namespace MinimalApiTemplate.Application.Features.TodoItems.EventHandlers.TodoItemCreated;

public class TodoItemCreatedEventHandler : INotificationHandler<TodoItemCreatedEvent>
{
    private readonly ILogger<TodoItemCreatedEventHandler> _logger;

    public TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public ValueTask Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", nameof(TodoItemCreatedEvent));

        return ValueTask.CompletedTask;
    }
}
