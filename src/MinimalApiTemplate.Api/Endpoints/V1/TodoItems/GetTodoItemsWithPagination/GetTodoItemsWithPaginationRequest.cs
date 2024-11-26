using System.ComponentModel;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetTodoItemsWithPagination;

public class GetTodoItemsWithPaginationRequest
{
    [DefaultValue(1)]
    public int PageNumber { get; set; }

    [DefaultValue(10)]
    public int PageSize { get; set; }

    public string[]? Tags { get; set; }
}
