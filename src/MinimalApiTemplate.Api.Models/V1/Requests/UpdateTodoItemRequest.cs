namespace MinimalApiTemplate.Api.Models.V1.Requests;

public class UpdateTodoItemRequest
{
    public string Title { get; set; } = null!;
    public PriorityLevel Priority { get; set; }
    public string? Note { get; set; }
}
