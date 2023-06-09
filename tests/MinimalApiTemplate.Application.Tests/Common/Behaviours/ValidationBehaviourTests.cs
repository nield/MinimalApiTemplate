﻿using FluentValidation.Results;
using MinimalApiTemplate.Application.Common.Behaviours;
using MediatR;

namespace MinimalApiTemplate.Application.Tests.Common.Behaviours;

public class ValidationBehaviourTests
{
    private readonly ValidationBehaviour<ValidationBehaviourInput, Unit> _validationBehaviour;
    private readonly Mock<RequestHandlerDelegate<Unit>> _pipelineBehaviourDelegateMock = new();
    private readonly Mock<IValidator<ValidationBehaviourInput>> _validatorMock = new();

    public ValidationBehaviourTests()
    {
        var failures = new List<ValidationFailure>
        {
            { new ValidationFailure("prop1", "required") }
        };

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<IValidationContext>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(failures));

        _validationBehaviour = new(new[] { _validatorMock.Object });
    }

    [Fact]
    public async Task When_ValidationExists_Then_ReturnTheErrorMessage()
    {
        _pipelineBehaviourDelegateMock.Setup(m => m())
            .ReturnsAsync(() => Unit.Value).Verifiable();

        var sut = await Assert.ThrowsAsync<Application.Common.Exceptions.ValidationException> (() => _validationBehaviour.Handle(new ValidationBehaviourInput(),                                          
                                            _pipelineBehaviourDelegateMock.Object,
                                            CancellationToken.None));

        sut.Errors.Count.Should().Be(1);
    }
}

public class ValidationBehaviourInput : IRequest<Unit>
{

}

public class ValidationBehaviourHandler : IRequestHandler<ValidationBehaviourInput, Unit>
{
    public Task<Unit> Handle(ValidationBehaviourInput request, CancellationToken cancellationToken)
    {
        return Unit.Task;
    }
}