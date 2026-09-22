using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Application.Features.TodoItems.EventHandlers.TodoItemCreated;

[Mapper]
public static partial class TodoItemCreatedEventMapper
{
    [MapperIgnoreTarget(nameof(ToDoItemCreated.CorrelationId))]
    public static partial ToDoItemCreated MapToDoItemCreated(this TodoItemCreatedEvent source);
}