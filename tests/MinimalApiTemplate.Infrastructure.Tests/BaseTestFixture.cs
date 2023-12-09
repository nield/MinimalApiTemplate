using MediatR;

namespace MinimalApiTemplate.Infrastructure.Tests;

public abstract class BaseTestFixture<T> : BaseTestFixture where T : class
{
    protected readonly ILogger<T> _loggerMock = Substitute.For<ILogger<T>>();
}

public abstract class BaseTestFixture
{
    protected readonly IMediator _mediatorMock = Substitute.For<IMediator>();
}
