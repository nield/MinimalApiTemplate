namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.GetTodoItemsWithPagination;

public class GetToDoItemsResponse
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public bool IsDone { get; set; }

    public List<string> Tags { get; set; } = [];
}
