using MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.CreateToDoItem;

[Mapper]
public static partial class Mapper
{
    public static partial CreateTodoItemCommand MapToCommand(this Request source);
}
