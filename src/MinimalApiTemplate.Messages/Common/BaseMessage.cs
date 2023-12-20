namespace MinimalApiTemplate.Messages.Common;

public abstract class BaseMessage
{
    public DateTimeOffset CreatedDateTime { get; } = DateTimeOffset.UtcNow;
}

