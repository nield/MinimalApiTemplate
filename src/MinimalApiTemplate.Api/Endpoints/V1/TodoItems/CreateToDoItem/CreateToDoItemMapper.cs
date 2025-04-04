using MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.CreateToDoItem;

[Mapper]
public static partial class CreateToDoItemMapper
{
    public static partial CreateTodoItemCommand MapCreateTodoItemCommand(this CreateTodoItemRequest source);
}
