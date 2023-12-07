namespace MinimalApiTemplate.Api.Models.V1.Requests;

public class CreateTodoItemRequest
{
    public required string Title { get; set; }

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTimeOffset? Reminder { get; set; }

    public List<string>? Tags { get; set; }
}
