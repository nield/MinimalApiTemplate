
using MinimalApiTemplate.Application.Common.Interfaces.Metrics;

namespace MinimalApiTemplate.Application.Tests;

[Collection("Mapping collection")]
public abstract class BaseTestFixture<T> : BaseTestFixture where T : class
{
    protected readonly Mock<ILogger<T>> _logger = new();
    protected readonly Mock<IToDoItemRepository> _templateRepositoryMock = new();
    protected readonly Mock<IToDoItemMetrics> _toDoMetricMock = new();

    protected BaseTestFixture(MappingFixture mappingFixture)
        : base(mappingFixture)
    {

    }

    protected override void DisposeCore()
    {
        base.DisposeCore();

        _templateRepositoryMock.VerifyAll();
        _toDoMetricMock.VerifyAll();
    }
}

[Collection("Mapping collection")]
public abstract class BaseTestFixture : IDisposable
{
    protected readonly IMapper _mapper;
    protected readonly Mock<IApplicationDbContext> _applicationDbContextMock = new();
    protected readonly Mock<IPublishMessageService> _publishMessageServiceMock = new();
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
