using Mediator;
using MinimalApiTemplate.Application.Common.Behaviours;
using NSubstitute.ExceptionExtensions;

namespace MinimalApiTemplate.Application.Tests.Common.Behaviours;

public class UnhandledExceptionBehaviourTests
{
    private readonly UnhandledExceptionBehaviour<UnhandledExceptionBehaviourTestInput, Unit> _unhandledExceptionBehaviour;
    private readonly ILogger<UnhandledExceptionBehaviourTestInput> _loggerMock = Substitute.For<ILogger<UnhandledExceptionBehaviourTestInput>>();
    private readonly MessageHandlerDelegate<UnhandledExceptionBehaviourTestInput, Unit> _pipelineBehaviourDelegateMock = Substitute.For<MessageHandlerDelegate<UnhandledExceptionBehaviourTestInput, Unit>>();


    public UnhandledExceptionBehaviourTests()
    {
        _unhandledExceptionBehaviour = new(_loggerMock);
    }

    [Fact]
    public async Task When_UnhandledExceptionIsThrown_Then_LogTheErrorMessage()
    {
        _pipelineBehaviourDelegateMock.Invoke(Arg.Any<UnhandledExceptionBehaviourTestInput>(), Arg.Any<CancellationToken>())
            .Throws(new Exception("Unhandled exception"));

        await Assert.ThrowsAsync<Exception>(() => _unhandledExceptionBehaviour.Handle(new UnhandledExceptionBehaviourTestInput(),
                                            CancellationToken.None,
                                            _pipelineBehaviourDelegateMock
                                            ).AsTask());

        _loggerMock.Received()
            .Log(LogLevel.Error, Arg.Any<EventId>(), Arg.Any<object>(),
                Arg.Any<Exception>(), Arg.Any<Func<object, Exception?, string>>());
    }
}

public class UnhandledExceptionBehaviourTestInput : IRequest
{

}