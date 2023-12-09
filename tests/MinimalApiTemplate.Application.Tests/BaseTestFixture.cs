using MinimalApiTemplate.Application.Common.Interfaces.Metrics;

namespace MinimalApiTemplate.Application.Tests;

[Collection("Mapping collection")]
public abstract class BaseTestFixture<T> : BaseTestFixture where T : class
{
    protected readonly ILogger<T> _logger = Substitute.For<ILogger<T>>();
    protected readonly IToDoItemRepository _templateRepositoryMock = Substitute.For<IToDoItemRepository>();
    protected readonly IToDoItemMetrics _toDoMetricMock = Substitute.For<IToDoItemMetrics>();

    protected BaseTestFixture(MappingFixture mappingFixture)
        : base(mappingFixture)
    {

    }
}

[Collection("Mapping collection")]
public abstract class BaseTestFixture : IDisposable
{
    protected readonly IMapper _mapper;
    protected readonly IApplicationDbContext _applicationDbContextMock = Substitute.For<IApplicationDbContext>();
    protected readonly IPublishMessageService _publishMessageServiceMock = Substitute.For<IPublishMessageService>();
    protected bool _disposedValue;

    protected BaseTestFixture(MappingFixture mappingFixture)
    {
        _mapper = mappingFixture.Mapper;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                DisposeCore();
            }

            _disposedValue = true;
        }
    }

    protected virtual void DisposeCore()
    {

    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
