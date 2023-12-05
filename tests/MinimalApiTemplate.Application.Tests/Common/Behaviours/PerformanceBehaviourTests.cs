using MinimalApiTemplate.Application.Common.Behaviours;
using MinimalApiTemplate.Application.Common.Settings;
using MediatR;
using Microsoft.Extensions.Options;

namespace MinimalApiTemplate.Application.Tests.Common.Behaviours;

public class PerformanceBehaviourTests
{
    private PerformanceBehaviour<PerformanceBehaviourTestInput, Unit>? _performanceBehaviour;
    private readonly Mock<ILogger<PerformanceBehaviourTestInput>> _loggerMock = new();
    private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
    private readonly Mock<RequestHandlerDelegate<Unit>> _pipelineBehaviourDelegateMock = new();

    public PerformanceBehaviourTests()
    {
        _currentUserServiceMock.Setup(x => x.UserId)
            .Returns("1");
    }

    public void Setup(AppSettings appSettings)
    {
        _performanceBehaviour = new(_loggerMock.Object,
                                        _currentUserServiceMock.Object,
                                       Options.Create<AppSettings>(appSettings));        
    }

    [Theory]
    [InlineData(false, 1, false, 0)]
    [InlineData(true, 10000, false, 5)]
    [InlineData(true, 1, true, 10)]
    public async Task When_ConditionsAreMet_Then_LogSlowRunningRequests(bool logRequests, int threshold, bool shouldHaveLog, int delay)
    {
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

        await _performanceBehaviour.Handle(
            new PerformanceBehaviourTestInput 
            {
                DelayInMilliseconds = delay 
            },                                            
            _pipelineBehaviourDelegateMock.Object,
            CancellationToken.None);

        _loggerMock.Verify(x => x.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => true),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
            shouldHaveLog ? Times.Once : Times.Never);
    }
}

public class PerformanceBehaviourTestInput : IRequest<Unit>
{
    public int DelayInMilliseconds { get; set; }
}

public class PerformanceBehaviourTestHandler : IRequestHandler<PerformanceBehaviourTestInput, Unit>
{
    public async Task<Unit> Handle(PerformanceBehaviourTestInput request, CancellationToken cancellationToken)
    {
        await Task.Delay(request.DelayInMilliseconds, cancellationToken);

        return Unit.Value;
    }
}

