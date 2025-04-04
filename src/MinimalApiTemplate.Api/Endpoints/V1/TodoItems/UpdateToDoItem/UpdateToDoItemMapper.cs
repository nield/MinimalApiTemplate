using MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.UpdateToDoItem;

[Mapper]
public static partial class UpdateToDoItemMapper
{
    public static partial UpdateTodoItemCommand MapToCommand(this UpdateTodoItemRequest source, long id);
}
