using MediatR;
using Microsoft.AspNetCore.OutputCaching;

namespace MinimalApiTemplate.Api.Tests;

public abstract class BaseTestFixture<T> : BaseTestFixture where T : class
{
    protected readonly ILogger<T> _loggerMock = Substitute.For<ILogger<T>>();
}

public abstract class BaseTestFixture
{
    protected readonly ISender _senderMock = Substitute.For<ISender>();
    protected readonly IOutputCacheStore _outputCacheStoreMock = Substitute.For<IOutputCacheStore>();
}
