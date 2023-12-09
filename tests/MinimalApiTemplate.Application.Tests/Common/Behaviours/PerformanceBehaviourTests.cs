using MediatR;
using Microsoft.Extensions.Options;
using MinimalApiTemplate.Application.Common.Behaviours;
using MinimalApiTemplate.Application.Common.Settings;

namespace MinimalApiTemplate.Application.Tests.Common.Behaviours;

public class PerformanceBehaviourTests
{
    private PerformanceBehaviour<PerformanceBehaviourTestInput, Unit>? _performanceBehaviour;
    private readonly ILogger<PerformanceBehaviourTestInput> _loggerMock = Substitute.For<ILogger<PerformanceBehaviourTestInput>>();
    private readonly ICurrentUserService _currentUserServiceMock = Substitute.For<ICurrentUserService>();
    private RequestHandlerDelegate<Unit> _pipelineBehaviourDelegateMock;

    public PerformanceBehaviourTests()
    {
        _currentUserServiceMock.UserId
            .Returns("1");       
    }

    public void Setup(AppSettings appSettings)
    {
        _performanceBehaviour = new(_loggerMock,
                                    _currentUserServiceMock,
                                    Options.Create(appSettings));
    }

    [Theory]    
    [InlineData(false, 1, false, 0)]
    [InlineData(true, 10000, false, 5)]
    [InlineData(true, 1, true, 10)]
    public async Task When_ConditionsAreMet_Then_LogSlowRunningRequests(
        bool logRequests, int threshold, bool shouldHaveLog, int delay)
    {
        _pipelineBehaviourDelegateMock = new RequestHandlerDelegate<Unit>(async () =>
        {
            await Task.Delay(delay);

            return Unit.Value;
        });

        Setup(new AppSettings
        {
            Logs = new Logs
            {
                Performance = new Performance
                {
                    LogSlowRunningHandlers = logRequests,
                    SlowRunningHandlerThreshold = threshold
                }
            }
        });

        if (_performanceBehaviour is null) throw new NullReferenceException("Setup was not called");

        await _performanceBehaviour.Handle(new PerformanceBehaviourTestInput(),
            _pipelineBehaviourDelegateMock,
            CancellationToken.None);

        _loggerMock.Received(shouldHaveLog ? 1 : 0)
            .Log(LogLevel.Warning, Arg.Any<EventId>(), Arg.Any<object>(),
                Arg.Any<Exception>(), Arg.Any<Func<object, Exception?, string>>());
    }
}

public class PerformanceBehaviourTestInput : IRequest<Unit>
{

}

public class PerformanceBehaviourTestHandler : IRequestHandler<PerformanceBehaviourTestInput, Unit>
{
    public Task<Unit> Handle(PerformanceBehaviourTestInput request, CancellationToken cancellationToken)
    {
        return Unit.Task;
    }
}

