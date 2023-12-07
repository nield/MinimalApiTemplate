using System.ComponentModel;

namespace MinimalApiTemplate.Api.Models.V1.Requests;

public class GetTodoItemsWithPaginationRequest
{
    [DefaultValue(1)]
    public int PageNumber { get; set; }

    [DefaultValue(10)]
    public int PageSize { get; set; }

    public string[]? Tags { get; set; }
}
