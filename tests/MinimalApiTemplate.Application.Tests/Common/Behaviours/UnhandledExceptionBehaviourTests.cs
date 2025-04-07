//using MediatR;
//using MinimalApiTemplate.Application.Common.Behaviours;
//using NSubstitute.ExceptionExtensions;

//namespace MinimalApiTemplate.Application.Tests.Common.Behaviours;

//public class UnhandledExceptionBehaviourTests
//{
//    private readonly UnhandledExceptionBehaviour<UnhandledExceptionBehaviourTestInput, Unit> _unhandledExceptionBehaviour;
//    private readonly ILogger<UnhandledExceptionBehaviourTestInput> _loggerMock = Substitute.For<ILogger<UnhandledExceptionBehaviourTestInput>>();
//    private readonly RequestHandlerDelegate<Unit> _pipelineBehaviourDelegateMock = Substitute.For<RequestHandlerDelegate<Unit>>();


//    public UnhandledExceptionBehaviourTests()
//    {
//        _unhandledExceptionBehaviour = new(_loggerMock);
//    }

//    [Fact]
//    public async Task When_UnhandledExceptionIsThrown_Then_LogTheErrorMessage()
//    {
//        _pipelineBehaviourDelegateMock.Invoke()
//            .Throws(new Exception("Unhandled exception"));

//        await Assert.ThrowsAsync<Exception>(() => _unhandledExceptionBehaviour.Handle(new UnhandledExceptionBehaviourTestInput(),
//                                            _pipelineBehaviourDelegateMock,
//                                            CancellationToken.None));

//        _loggerMock.Received()
//            .Log(LogLevel.Error, Arg.Any<EventId>(), Arg.Any<object>(),
//                Arg.Any<Exception>(), Arg.Any<Func<object, Exception?, string>>());
//    }
//}

//public class UnhandledExceptionBehaviourTestInput : IRequest<Unit>
//{

//}

//public class UnhandledExceptionBehaviourTestHandler : IRequestHandler<UnhandledExceptionBehaviourTestInput, Unit>
//{
//    public Task<Unit> Handle(UnhandledExceptionBehaviourTestInput request, CancellationToken cancellationToken)
//    {
//        return Unit.Task;
//    }
//}