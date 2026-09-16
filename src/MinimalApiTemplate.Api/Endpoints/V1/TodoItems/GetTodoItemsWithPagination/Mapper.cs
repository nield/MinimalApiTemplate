using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetTodoItemsWithPagination;

[Mapper]
public static partial class Mapper
{
    public static partial GetTodoItemsWithPaginationQuery MapToQuery(this Request source);    
    
    public static partial Response MapToResponse(GetTodoItemsDto source);
}
