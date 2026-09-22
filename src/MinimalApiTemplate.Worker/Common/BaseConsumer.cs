namespace MinimalApiTemplate.Worker.Common;

public abstract class BaseConsumer<TMessage> : IHandleMessages<TMessage>
    where TMessage : BaseMessage
{
    protected readonly ILogger _logger;

    protected BaseConsumer(ILogger logger)
    {
        _logger = logger;
    }

    protected abstract Task ProcessMessage(TMessage message);

    public async Task Handle(TMessage message)
    {
        using (Serilog.Context.LogContext.PushProperty("CorrelationId", message.CorrelationId))
        {
            try
            {
                await ProcessMessage(message);
            }
#pragma warning disable S2139 // Exceptions should be either logged or rethrown but not both
            catch (Exception ex)
#pragma warning restore S2139 // Exceptions should be either logged or rethrown but not both
            {
                _logger.LogError(ex, "Failed to process message. Message: {Message}, CorrelationId: {CorrelationId}", message, message.CorrelationId);
                throw;
            }

        }
    }
}

