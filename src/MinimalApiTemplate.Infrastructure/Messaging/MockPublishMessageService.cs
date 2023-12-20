namespace MinimalApiTemplate.Infrastructure.Messaging;

public class MockPublishMessageService : IPublishMessageService
{
    public Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default) 
        where TMessage : BaseMessage
    {
        return Task.CompletedTask;
    }
}

