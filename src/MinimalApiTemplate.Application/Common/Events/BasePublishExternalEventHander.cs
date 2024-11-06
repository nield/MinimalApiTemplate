namespace MinimalApiTemplate.Application.Common.Events;

#pragma warning disable S2436 // Types and methods should not have too many generic parameters
public abstract class BasePublishExternalEventHander<TNotification, TMessage, THandler> : INotificationHandler<TNotification>
#pragma warning restore S2436 // Types and methods should not have too many generic parameters
    where TNotification : INotification
    where TMessage : BaseMessage
{
    private readonly IPublishMessageService _publishMessageService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<THandler> _logger;
    private readonly IMapper _mapper;

    protected BasePublishExternalEventHander(
        IPublishMessageService publishMessageService,
        ICurrentUserService currentUserService, 
        IMapper mapper,
#pragma warning disable S6672 // Generic logger injection should match enclosing type
        ILogger<THandler> logger)
#pragma warning restore S6672 // Generic logger injection should match enclosing type
    {
        _publishMessageService = publishMessageService;
        _currentUserService = currentUserService;
        _mapper = mapper;
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
            var message = _mapper.Map<TMessage>(notification);

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
