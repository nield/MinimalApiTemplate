using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MinimalApiTemplate.Application.Common.Settings;

namespace MinimalApiTemplate.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IMessage
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly IServiceScopeFactory _serviceScope;
    private readonly AppSettings _appSettings;

    public PerformanceBehaviour(
#pragma warning disable S6672 // Generic logger injection should match enclosing type
        ILogger<TRequest> logger,
#pragma warning restore S6672 // Generic logger injection should match enclosing type
        IServiceScopeFactory  serviceScope,
        IOptions<AppSettings> appSettings)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _serviceScope = serviceScope;
        _appSettings = appSettings.Value;
    }

    public async ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
    {

        _timer.Start();

        var response = await next(message, cancellationToken);

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (_appSettings.Logs.Performance.LogSlowRunningHandlers
            && elapsedMilliseconds >= _appSettings.Logs.Performance.SlowRunningHandlerThreshold)
        {
            using var scope = _serviceScope.CreateScope();

            var requestName = typeof(TRequest).Name;

            var currentUserService = scope.ServiceProvider.GetRequiredService<ICurrentUserService>();
            var userId = currentUserService.UserId ?? string.Empty;

            _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
                requestName, elapsedMilliseconds, userId, message);
        }

        return response;
    }
}
