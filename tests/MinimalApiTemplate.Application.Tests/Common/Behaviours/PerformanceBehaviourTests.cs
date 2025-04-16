using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MinimalApiTemplate.Application.Common.Behaviours;
using MinimalApiTemplate.Application.Common.Settings;

namespace MinimalApiTemplate.Application.Tests.Common.Behaviours;

public class PerformanceBehaviourTests
{
    private PerformanceBehaviour<PerformanceBehaviourTestInput, Unit>? _performanceBehaviour;
    private readonly ILogger<PerformanceBehaviourTestInput> _loggerMock = Substitute.For<ILogger<PerformanceBehaviourTestInput>>();
    private readonly ICurrentUserService _currentUserServiceMock = Substitute.For<ICurrentUserService>();
    private readonly IServiceScopeFactory _serviceScopeFactoryMock = Substitute.For<IServiceScopeFactory>();
    private readonly IServiceScope _serviceScopeMock = Substitute.For<IServiceScope>();
    private MessageHandlerDelegate<PerformanceBehaviourTestInput, Unit>? _pipelineBehaviourDelegateMock = null;

    public PerformanceBehaviourTests()
    {
        _serviceScopeFactoryMock.CreateScope().Returns(_serviceScopeMock);

        _serviceScopeMock.ServiceProvider.GetService<ICurrentUserService>().Returns(_currentUserServiceMock);

        _currentUserServiceMock.UserId
            .Returns("1");
    }

    public void Setup(AppSettings appSettings)
    {
        _performanceBehaviour = new(_loggerMock,
                                    _serviceScopeFactoryMock,
                                    Options.Create(appSettings));
    }

    [Theory]
    [InlineData(false, 1, false, 0)]
    [InlineData(true, 10000, false, 5)]
    [InlineData(true, 1, true, 10)]
    public async Task When_ConditionsAreMet_Then_LogSlowRunningRequests(
        bool logRequests, int threshold, bool shouldHaveLog, int delay)
    {
        _pipelineBehaviourDelegateMock = new MessageHandlerDelegate<PerformanceBehaviourTestInput, Unit>(async (input, ct) =>
        {
            await Task.Delay(delay, ct);

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
            CancellationToken.None,
            _pipelineBehaviourDelegateMock
            );

        _loggerMock.Received(shouldHaveLog ? 1 : 0)
            .Log(LogLevel.Warning, Arg.Any<EventId>(), Arg.Any<object>(),
                Arg.Any<Exception>(), Arg.Any<Func<object, Exception?, string>>());
    }
}

public class PerformanceBehaviourTestInput : IRequest
{

}
