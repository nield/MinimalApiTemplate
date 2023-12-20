namespace MinimalApiTemplate.Messages.V1;

public class ToDoItemCreated : BaseMessage
{
    public required string Title { get; set; }

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTimeOffset? Reminder { get; set; }
}

