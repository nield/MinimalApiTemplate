namespace MinimalApiTemplate.Application.Common.Interfaces;

public interface IPublishMessageService
{
    Task Publish<TNotification, TMessage>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
        where TMessage : class;
}
