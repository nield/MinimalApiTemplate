namespace MinimalApiTemplate.Worker.Common;

public abstract class BaseConsumer<TMessage> : IConsumer<TMessage>
    where TMessage : BaseMessage
{
    protected readonly ILogger _logger;

    protected BaseConsumer(ILogger logger)
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
#pragma warning disable S2139 // Exceptions should be either logged or rethrown but not both
            catch (Exception ex)
#pragma warning restore S2139 // Exceptions should be either logged or rethrown but not both
            {
                _logger.LogError(ex, "Failed to process message. Message: {Message}, CorrelationId: {CorrelationId}", context.Message, context.CorrelationId);
                throw;
            }

        }
    }
}

