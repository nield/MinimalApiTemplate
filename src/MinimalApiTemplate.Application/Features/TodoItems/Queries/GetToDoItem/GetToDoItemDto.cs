namespace MinimalApiTemplate.Application.Features.TodoItems.Queries.GetToDoItem;
public class GetToDoItemDto
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTimeOffset? Reminder { get; set; }

    public bool IsDone { get; set; }
}
