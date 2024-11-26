using MinimalApiTemplate.Application.Features.TodoItems.Commands.UpdateTodoItem;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.UpdateToDoItem;

public class UpdateToDoItemMapper : Profile
{
    public UpdateToDoItemMapper()
    {
        CreateMap<UpdateTodoItemRequest, UpdateTodoItemCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
