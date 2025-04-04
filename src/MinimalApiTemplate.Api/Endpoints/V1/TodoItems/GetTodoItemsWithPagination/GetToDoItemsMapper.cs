using MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetTodoItemsWithPagination;

[Mapper]
public static partial class GetToDoItemsMapper
{
    public static partial GetTodoItemsWithPaginationQuery MapGetTodoItemsWithPaginationQuery(this GetTodoItemsWithPaginationRequest source);
    
}
