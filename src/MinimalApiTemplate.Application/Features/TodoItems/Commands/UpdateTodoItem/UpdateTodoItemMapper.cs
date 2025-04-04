namespace MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;

[Mapper]
public static partial class UpdateTodoItemMapper
{
    [MapperIgnoreTarget(nameof(TodoItem.IsDone))]
    [MapperIgnoreTarget(nameof(TodoItem.CreatedDateTime))]
    [MapperIgnoreTarget(nameof(TodoItem.CreatedBy))]
    [MapperIgnoreTarget(nameof(TodoItem.LastModifiedBy))]
    [MapperIgnoreTarget(nameof(TodoItem.LastModifiedDateTime))]
    [MapperIgnoreTarget(nameof(TodoItem.IsDeleted))]
    [MapperIgnoreTarget(nameof(TodoItem.Reminder))]
    public static partial void MapToDoItem(this UpdateTodoItemCommand source, TodoItem destination);
}
