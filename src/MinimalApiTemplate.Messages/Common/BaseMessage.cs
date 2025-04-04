namespace MinimalApiTemplate.Messages.Common;

public abstract class BaseMessage
{
    public string? CorrelationId { get; set; }
    public DateTimeOffset CreatedDateTime { get; } = DateTimeOffset.UtcNow;
}

