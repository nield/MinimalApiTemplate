using MinimalApiTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.CreateToDoItem;

public class CreateToDoItemMapper : Profile
{
    public CreateToDoItemMapper()
    {
        CreateMap<CreateTodoItemRequest, CreateTodoItemCommand>();
    }
}
