using Microsoft.Extensions.Options;
using MinimalApiTemplate.Application.Common.Settings;

namespace MinimalApiTemplate.Infrastructure.Messaging;

public class PublishMessageService : IPublishMessageService
{
    private readonly IBus _bus;
    private readonly MessagingSettings _messagingSettings;
    private readonly ILogger<PublishMessageService> _logger;

    public PublishMessageService(
        IBus bus,
        IOptions<MessagingSettings> messagingSettings,
        ILogger<PublishMessageService> logger)
    {
        _bus = bus;
        _messagingSettings = messagingSettings.Value;
        _logger = logger;
    }

    public async Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : BaseMessage
    {
        if (!_messagingSettings.PublishEnabled) return;

        try
        {
            await _bus.Publish(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed publishing message. Message: {Message}. CorrelationId: {CorrelationId}", message, message.CorrelationId);
            throw;
        }
    }
}