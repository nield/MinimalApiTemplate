namespace MinimalApiTemplate.Domain.Events;

public class TodoItemCreatedEvent : BaseEvent
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public string? Note { get; set; }
    public PriorityLevel Priority { get; set; }
    public DateTimeOffset? Reminder { get; set; }
}
