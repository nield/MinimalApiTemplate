namespace MinimalApiTemplate.Messages.Common;

public abstract class BaseMessage
{
    public required string CorrelationId { get; set; }
    public DateTimeOffset CreatedDateTime { get; } = DateTimeOffset.UtcNow;
}

