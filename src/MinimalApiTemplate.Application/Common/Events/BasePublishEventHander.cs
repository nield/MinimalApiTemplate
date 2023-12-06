namespace MinimalApiTemplate.Application.Common.Events;

public abstract class BasePublishEventHander<TNotification, TMessage> : INotificationHandler<TNotification>
    where TNotification : INotification
    where TMessage : class
{
    private readonly IPublishMessageService _publishMessageService;

    protected BasePublishEventHander(IPublishMessageService publishMessageService)
    {
        _publishMessageService = publishMessageService;
    }

    public virtual async Task Handle(TNotification notification, CancellationToken cancellationToken)
    {
        await _publishMessageService.Publish<TNotification, TMessage>(notification, cancellationToken);
    }
}
