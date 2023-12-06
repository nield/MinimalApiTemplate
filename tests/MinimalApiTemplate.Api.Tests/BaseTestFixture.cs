
using MediatR;
using Microsoft.AspNetCore.OutputCaching;

namespace MinimalApiTemplate.Api.Tests;

[Collection("Mapping collection")]
public abstract class BaseTestFixture<T> : BaseTestFixture where T : class
{
    protected readonly Mock<ILogger<T>> _loggerMock = new();

    protected BaseTestFixture(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
    }
}

[Collection("Mapping collection")]
public abstract class BaseTestFixture
{
    protected readonly IMapper _mapper;
    protected readonly Mock<ISender> _senderMock = new();
    protected readonly Mock<IOutputCacheStore> _outputCacheStoreMock = new();

    protected BaseTestFixture(MappingFixture mappingFixture)
    {
        _mapper = mappingFixture.Mapper;
    }
}
