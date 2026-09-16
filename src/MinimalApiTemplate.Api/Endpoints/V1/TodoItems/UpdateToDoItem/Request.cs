namespace MinimalApiTemplate.Api.Endpoints.V1.TodoItems.UpdateToDoItem;

public class Request
{
    public required string Title { get; set; }

    public PriorityLevel Priority { get; set; }

    public string? Note { get; set; }

    public List<string>? Tags { get; set; }
}
