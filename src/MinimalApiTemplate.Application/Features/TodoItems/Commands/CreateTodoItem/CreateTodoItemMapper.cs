namespace MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;

[Mapper]
public static partial class CreateTodoItemMapper
{
    [MapperIgnoreTarget(nameof(TodoItem.Id))]
    [MapperIgnoreTarget(nameof(TodoItem.IsDone))]
    [MapperIgnoreTarget(nameof(TodoItem.CreatedDateTime))]
    [MapperIgnoreTarget(nameof(TodoItem.CreatedBy))]
    [MapperIgnoreTarget(nameof(TodoItem.LastModifiedBy))]
    [MapperIgnoreTarget(nameof(TodoItem.LastModifiedDateTime))]
    [MapperIgnoreTarget(nameof(TodoItem.IsDeleted))]
    public static partial TodoItem MapTodoItem(this CreateTodoItemCommand source);

    [MapperIgnoreSource(nameof(TodoItem.IsDone))]
    [MapperIgnoreSource(nameof(TodoItem.CreatedDateTime))]
    [MapperIgnoreSource(nameof(TodoItem.CreatedBy))]
    [MapperIgnoreSource(nameof(TodoItem.LastModifiedBy))]
    [MapperIgnoreSource(nameof(TodoItem.LastModifiedDateTime))]
    [MapperIgnoreSource(nameof(TodoItem.IsDeleted))]
    [MapperIgnoreSource(nameof(TodoItem.Tags))]
    [MapperIgnoreSource(nameof(TodoItem.DomainEvents))]
    public static partial TodoItemCreatedEvent MapTodoItemCreatedEvent(this TodoItem source);
}
