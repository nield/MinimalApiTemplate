namespace MinimalApiTemplate.Application.Common.Interfaces;

public interface IPublishMessageService
{
    Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : BaseMessage;
}
