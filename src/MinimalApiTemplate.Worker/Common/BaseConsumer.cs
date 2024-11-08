﻿namespace MinimalApiTemplate.Worker.Common;

public abstract class BaseConsumer<TMessage, TConsumer> : IConsumer<TMessage>
    where TMessage : BaseMessage
    where TConsumer : IConsumer<TMessage>
{
    protected readonly ILogger<TConsumer> _logger;

    protected BaseConsumer(
#pragma warning disable S6672 // Generic logger injection should match enclosing type
        ILogger<TConsumer> logger
#pragma warning restore S6672 // Generic logger injection should match enclosing type
        )

    {
        _logger = logger;
    }

    protected abstract Task ProcessMessage(ConsumeContext<TMessage> context);

    public async Task Consume(ConsumeContext<TMessage> context)
    {
        using (Serilog.Context.LogContext.PushProperty("CorrelationId", context.CorrelationId))
        {
            try
            {
                await ProcessMessage(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process message. Message: {Message}, CorrelationId: {CorrelationId}", context.Message, context.CorrelationId);
                throw;
            }
        }
    }
}

