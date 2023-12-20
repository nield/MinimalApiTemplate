namespace MinimalApiTemplate.Application.Common.Events;

public abstract class BasePublishExternalEventHander<TNotification, TMessage, THandler> : INotificationHandler<TNotification>
    where TNotification : INotification
    where TMessage : BaseMessage
{
    private readonly IPublishMessageService _publishMessageService;
    private readonly ILogger<THandler> _logger;
    private readonly IMapper _mapper;

    protected BasePublishExternalEventHander(IPublishMessageService publishMessageService,
        IMapper mapper, ILogger<THandler> logger)
    {
        _publishMessageService = publishMessageService;
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
            return _mapper.Map<TMessage>(notification);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, @"Could not map notification type '{Notification}' 
                                        to message type '{MessageType}'", typeof(TNotification).Name, typeof(TMessage).Name);

            throw new InvalidMappingException(typeof(TNotification), typeof(TMessage));
        }
    }
}
