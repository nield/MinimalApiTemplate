using System.ComponentModel;

namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetTodoItemsWithPagination;

public class GetTodoItemsWithPaginationRequest
{
    [DefaultValue(1)]
    [Description("The current page number. The first page is 1")]
    public int PageNumber { get; set; }

    [DefaultValue(10)]
    [Description("The number of records on a page")]
    public int PageSize { get; set; }

    [Description("The tags to filter on. Not required")]
    public string[]? Tags { get; set; }
}
