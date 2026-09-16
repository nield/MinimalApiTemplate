using MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.UpdateToDoItem;

[Mapper]
public static partial class Mapper
{
    public static partial UpdateTodoItemCommand MapToCommand(this Request source, long id);
}
