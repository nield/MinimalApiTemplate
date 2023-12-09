using MediatR;
using Microsoft.AspNetCore.OutputCaching;

namespace MinimalApiTemplate.Api.Tests;

[Collection("Mapping collection")]
public abstract class BaseTestFixture<T> : BaseTestFixture where T : class
{
    protected readonly ILogger<T> _loggerMock = Substitute.For<ILogger<T>>();

    protected BaseTestFixture(MappingFixture mappingFixture)
        : base(mappingFixture)
    {
    }
}

[Collection("Mapping collection")]
public abstract class BaseTestFixture
{
    protected readonly IMapper _mapper;
    protected readonly ISender _senderMock = Substitute.For<ISender>();
    protected readonly IOutputCacheStore _outputCacheStoreMock = Substitute.For<IOutputCacheStore>();

    protected BaseTestFixture(MappingFixture mappingFixture)
    {
        _mapper = mappingFixture.Mapper;
    }
}
