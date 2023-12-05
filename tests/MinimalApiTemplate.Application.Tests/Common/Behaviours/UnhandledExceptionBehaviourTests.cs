using MinimalApiTemplate.Application.Common.Behaviours;
using MediatR;

namespace MinimalApiTemplate.Application.Tests.Common.Behaviours;

public class UnhandledExceptionBehaviourTests
{
    private readonly UnhandledExceptionBehaviour<UnhandledExceptionBehaviourTestInput, Unit> _unhandledExceptionBehaviour;
    private readonly Mock<ILogger<UnhandledExceptionBehaviourTestInput>> _loggerMock = new();
    private readonly Mock<RequestHandlerDelegate<Unit>> _pipelineBehaviourDelegateMock = new();


    public UnhandledExceptionBehaviourTests()
    {
        _unhandledExceptionBehaviour = new(_loggerMock.Object);
    }

    [Fact]
    public async Task When_UnhandledExceptionIsThrown_Then_LogTheErrorMessage()
    {
        _pipelineBehaviourDelegateMock.Setup(m => m())
            .ThrowsAsync(new Exception("Unhandled exception")).Verifiable();

        await Assert.ThrowsAsync<Exception>(() => _unhandledExceptionBehaviour.Handle(new UnhandledExceptionBehaviourTestInput(),
                                            _pipelineBehaviourDelegateMock.Object,
                                            CancellationToken.None));

        _loggerMock.Verify(x => x.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => true),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
            Times.Once);
    }
}

public class UnhandledExceptionBehaviourTestInput : IRequest<Unit>
{

}

public class UnhandledExceptionBehaviourTestHandler : IRequestHandler<UnhandledExceptionBehaviourTestInput, Unit>
{
    public Task<Unit> Handle(UnhandledExceptionBehaviourTestInput request, CancellationToken cancellationToken)
    {
        return Unit.Task;
    }
}