namespace MinimalApiTemplate.Domain.Events;

public class TodoItemDeletedEvent : BaseEvent
{
    public long Id { get; set; }
}
