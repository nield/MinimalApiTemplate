using MinimalApiTemplate.Messages.V1.Enums;

namespace MinimalApiTemplate.Messages.V1;

public class ToDoItemCreated
{
    public string Title { get; set; } = null!;

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTimeOffset? Reminder { get; set; }
}
