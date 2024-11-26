using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

namespace MinimalApiTemplate.Api.Endpoints.V2.ToDoItems.GetToDoItem;

public class GetToDoItemMapper : Profile
{
    public GetToDoItemMapper()
    {
        CreateMap<GetToDoItemDto, GetToDoItemResponse>();
    }
}
