using MinimalApiTemplate.Application.Common.Events;
using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Application.Features.TodoItems.EventHandlers.TodoItemDeleted;

public class TodoItemDeletedEventExternalHandler
    : BasePublishExternalEventHander<TodoItemDeletedEvent, ToDoItemDeleted>
{
    public TodoItemDeletedEventExternalHandler(
        IPublishMessageService publishMessageService, 
        ICurrentUserService currentUserService) 
        : base(publishMessageService, currentUserService)
    {
    }

    protected override ToDoItemDeleted MapMessage(TodoItemDeletedEvent notification) =>
        notification.MapToDoItemDeleted();
}