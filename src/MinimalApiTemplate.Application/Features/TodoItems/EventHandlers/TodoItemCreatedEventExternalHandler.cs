using MinimalApiTemplate.Application.Common.Events;
using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Application.Features.TodoItems.EventHandlers;

public class TodoItemCreatedEventExternalHandler 
    : BasePublishExternalEventHander<TodoItemCreatedEvent, ToDoItemCreated, TodoItemCreatedEventExternalHandler>
{
    public TodoItemCreatedEventExternalHandler(
        IPublishMessageService publishMessageService, 
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<TodoItemCreatedEventExternalHandler> logger) 
        : base(publishMessageService, currentUserService, mapper, logger)
    {
    }
}

