namespace MinimalApiTemplate.Api.Models.V1.Requests;

public class CreateTodoItemRequest
{
    public string Title { get; set; } = null!;

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTimeOffset? Reminder { get; set; }
}
