using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Application.Features.TodoItems.EventHandlers.TodoItemDeleted;

[Mapper]
public static partial class TodoItemDeletedEventMapper
{
    [MapperIgnoreTarget(nameof(ToDoItemCreated.CorrelationId))]
    public static partial ToDoItemDeleted MapToDoItemDeleted(this TodoItemDeletedEvent source);
}