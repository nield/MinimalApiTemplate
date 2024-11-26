namespace MinimalApiTemplate.Application.Features.TodoItems.EventHandlers.TodoItemDeleted;

public class TodoItemDeletedEventHandler : INotificationHandler<TodoItemDeletedEvent>
{
    private readonly ILogger<TodoItemDeletedEventHandler> _logger;

    public TodoItemDeletedEventHandler(ILogger<TodoItemDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", nameof(TodoItemDeletedEvent));

        return Task.CompletedTask;
    }
}
