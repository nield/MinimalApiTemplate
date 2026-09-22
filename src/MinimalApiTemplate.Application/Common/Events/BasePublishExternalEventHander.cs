namespace MinimalApiTemplate.Application.Common.Events;

public abstract class BasePublishExternalEventHander<TNotification, TMessage> 
    : INotificationHandler<TNotification>
        where TNotification : INotification
        where TMessage : BaseMessage
{
    private readonly IPublishMessageService _publishMessageService;
    private readonly ICurrentUserService _currentUserService;

    protected BasePublishExternalEventHander(
        IPublishMessageService publishMessageService,
        ICurrentUserService currentUserService)
    {
        _publishMessageService = publishMessageService;
        _currentUserService = currentUserService;
    }

    public virtual async ValueTask Handle(TNotification notification, CancellationToken cancellationToken)
    {
        var message = MapMessage(notification);
        
        SetMessageDefaults(message);

        await _publishMessageService.Publish(message, cancellationToken);
    }

    protected abstract TMessage MapMessage(TNotification notification);

    protected virtual void SetMessageDefaults(TMessage message)
    {
        message.CorrelationId = _currentUserService.CorrelationId ?? Guid.NewGuid().ToString();
    }
}
