using MinimalApiTemplate.Application.Common.Events;
using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Application.Features.TodoItems.EventHandlers.TodoItemCreated;

public class TodoItemCreatedEventExternalHandler
    : BasePublishExternalEventHander<TodoItemCreatedEvent, ToDoItemCreated>
{
    public TodoItemCreatedEventExternalHandler(
        IPublishMessageService publishMessageService,
        ICurrentUserService currentUserService,
        ILogger<TodoItemCreatedEventExternalHandler> logger)
        : base(publishMessageService, currentUserService, logger)
    {
    }
}

