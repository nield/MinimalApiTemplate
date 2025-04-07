using FluentValidation.Results;
using Mediator;
using MinimalApiTemplate.Application.Common.Behaviours;

namespace MinimalApiTemplate.Application.Tests.Common.Behaviours;

public class ValidationBehaviourTests
{
    private readonly ValidationBehaviour<ValidationBehaviourTestInput, Unit> _validationBehaviour;
    private readonly MessageHandlerDelegate<ValidationBehaviourTestInput, Unit> _pipelineBehaviourDelegateMock = Substitute.For<MessageHandlerDelegate<ValidationBehaviourTestInput, Unit>>();
    private readonly IValidator<ValidationBehaviourTestInput> _validatorMock = Substitute.For<IValidator<ValidationBehaviourTestInput>>();

    public ValidationBehaviourTests()
    {
        var failures = new List<ValidationFailure>
        {
            { new ValidationFailure("prop1", "required") }
        };

        _validatorMock.ValidateAsync(Arg.Any<IValidationContext>(), Arg.Any<CancellationToken>())
            .Returns(new ValidationResult(failures));

        _validationBehaviour = new([_validatorMock]);
    }

    [Fact]
    public async Task When_ValidationExists_Then_ReturnTheErrorMessage()
    {
        _pipelineBehaviourDelegateMock.Invoke(Arg.Any<ValidationBehaviourTestInput>(), CancellationToken.None)
            .Returns(Unit.ValueTask);

        var sut = await Assert.ThrowsAsync<Application.Common.Exceptions.DataValidationFailureException>(() 
            => _validationBehaviour.Handle(new ValidationBehaviourTestInput(),
            CancellationToken.None,
            _pipelineBehaviourDelegateMock).AsTask());

        sut.Errors.Count.Should().Be(1);
    }
}

public class ValidationBehaviourTestInput : IRequest
{

}