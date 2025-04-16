namespace MinimalApiTemplate.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IMessage
{
    private readonly ILogger<TRequest> _logger;

#pragma warning disable S6672 // Generic logger injection should match enclosing type
    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
#pragma warning restore S6672 // Generic logger injection should match enclosing type
    {
        _logger = logger;
    }

    public async ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
    {
        try
        {
            return await next(message, cancellationToken);
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "Request: Unhandled Exception for Request {Name} {@Request}", requestName, message);

            throw;
        }
    }
}
