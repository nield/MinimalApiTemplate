namespace MinimalApiTemplate.Application.Common.Events;

public abstract class BasePublishExternalEventHander<TNotification, TMessage> 
    : INotificationHandler<TNotification>
        where TNotification : INotification
        where TMessage : BaseMessage
{
    private readonly IPublishMessageService _publishMessageService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger _logger;

    protected BasePublishExternalEventHander(
        IPublishMessageService publishMessageService,
        ICurrentUserService currentUserService, 
        ILogger logger)
    {
        _publishMessageService = publishMessageService;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public virtual async Task Handle(TNotification notification, CancellationToken cancellationToken)
    {
        var message = MapMessage(notification);

        await _publishMessageService.Publish(message, cancellationToken);
    }

    protected virtual TMessage MapMessage(TNotification notification)
    {
        try
        {
            var message = notification.MapToMessage<TNotification, TMessage>();

            SetMessageDefaults(message);

            return message;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, @"Could not map notification type '{Notification}' 
                                        to message type '{MessageType}'", typeof(TNotification).Name, typeof(TMessage).Name);

            throw new InvalidMappingException(typeof(TNotification), typeof(TMessage));
        }
    }

    protected void SetMessageDefaults(TMessage message)
    {
        message.CorrelationId = _currentUserService.CorrelationId ?? Guid.NewGuid().ToString();
    }
}
