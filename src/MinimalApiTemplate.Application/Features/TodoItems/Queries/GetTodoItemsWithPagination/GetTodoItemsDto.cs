namespace MinimalApiTemplate.Application.Features.TodoItems.Queries.GetTodoItemsWithPagination;

public class GetTodoItemsDto
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public bool IsDone { get; set; }
}
