namespace MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

public class GetTodoItemsDto
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public bool IsDone { get; set; }
}
