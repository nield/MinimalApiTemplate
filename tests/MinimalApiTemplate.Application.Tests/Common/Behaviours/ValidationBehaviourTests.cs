using FluentValidation.Results;
using MediatR;
using MinimalApiTemplate.Application.Common.Behaviours;

namespace MinimalApiTemplate.Application.Tests.Common.Behaviours;

public class ValidationBehaviourTests
{
    private readonly ValidationBehaviour<ValidationBehaviourTestInput, Unit> _validationBehaviour;
    private readonly RequestHandlerDelegate<Unit> _pipelineBehaviourDelegateMock = Substitute.For<RequestHandlerDelegate<Unit>>();
    private readonly IValidator<ValidationBehaviourTestInput> _validatorMock = Substitute.For<IValidator<ValidationBehaviourTestInput>>();

    public ValidationBehaviourTests()
    {
        var failures = new List<ValidationFailure>
        {
            { new ValidationFailure("prop1", "required") }
        };

        _validatorMock.ValidateAsync(Arg.Any<IValidationContext>(), Arg.Any<CancellationToken>())
            .Returns(new ValidationResult(failures));

        _validationBehaviour = new(new[] { _validatorMock });
    }

    [Fact]
    public async Task When_ValidationExists_Then_ReturnTheErrorMessage()
    {
        _pipelineBehaviourDelegateMock.Invoke()
            .Returns(Unit.Value);

        var sut = await Assert.ThrowsAsync<Application.Common.Exceptions.ValidationException>(() => _validationBehaviour.Handle(new ValidationBehaviourTestInput(),
                                            _pipelineBehaviourDelegateMock,
                                            CancellationToken.None));

        sut.Errors.Count.Should().Be(1);
    }
}

public class ValidationBehaviourTestInput : IRequest<Unit>
{

}

public class ValidationBehaviourTestHandler : IRequestHandler<ValidationBehaviourTestInput, Unit>
{
    public Task<Unit> Handle(ValidationBehaviourTestInput request, CancellationToken cancellationToken)
    {
        return Unit.Task;
    }
}