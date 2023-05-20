namespace MinimalApiTemplate.Domain.Entities;

public class TodoItem : BaseAuditableEntity
{
    public string Title { get; set; } = null!;

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTimeOffset? Reminder { get; set; }

    public bool IsDone { get; set; }
}
