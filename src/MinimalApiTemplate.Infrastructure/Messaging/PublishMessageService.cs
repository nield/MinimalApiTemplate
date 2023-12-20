using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MinimalApiTemplate.Application.Common.Settings;

namespace MinimalApiTemplate.Infrastructure.Messaging;

public class PublishMessageService : IPublishMessageService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly MassTransitSettings _massTransitSettings;

    private readonly ILogger<PublishMessageService> _logger;
    public PublishMessageService(IPublishEndpoint publishEndpoint,
        IOptions<MassTransitSettings> massTransitSettings,
        ILogger<PublishMessageService> logger)
    {
        _publishEndpoint = publishEndpoint;
        _massTransitSettings = massTransitSettings.Value;
        _logger = logger;
    }

    public async Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : BaseMessage
    {
        if (!_massTransitSettings.PublishEnabled) return;

        try
        {
            await _publishEndpoint.Publish(message, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed publishing message. Message: {Message}", message);
            throw;
        }
    }
}