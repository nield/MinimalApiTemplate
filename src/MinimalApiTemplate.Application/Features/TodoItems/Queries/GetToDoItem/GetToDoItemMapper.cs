namespace MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;

public class GetToDoItemMapper : Profile
{
    public GetToDoItemMapper()
    {
        CreateMap<TodoItem, GetToDoItemDto>();
    }
}
