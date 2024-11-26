namespace MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

public class GetTodoItemsMapper : Profile
{
    public GetTodoItemsMapper()
    {
        CreateMap<TodoItem, GetTodoItemsDto>();
    }
}
