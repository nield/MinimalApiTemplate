namespace MinimalApiTemplate.Api.Models.V1.Requests;

public class GetTodoItemsWithPaginationRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
