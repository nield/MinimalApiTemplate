using MinimalApiTemplate.Messages.V1;

namespace MinimalApiTemplate.Application.Features.TodoItems.EventHandlers.TodoItemCreated;

[Mapper]
public static partial class TodoItemCreatedMapper
{
    public static partial ToDoItemCreated MapToDoItemCreated(this TodoItemCreatedEvent source, string? correlationId);
}
