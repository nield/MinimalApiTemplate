using MediatR;
using MinimalApiTemplate.Domain.Common;
using MinimalApiTemplate.Infrastructure.Persistence.Interceptors;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence.Interceptors;

public class DispatchDomainEventsInterceptorTests
{
    private readonly DispatchDomainEventsInterceptor _interceptor;
    private readonly Mock<IMediator> _mediatorMock = new();

    public DispatchDomainEventsInterceptorTests()
    {
        _interceptor = new(_mediatorMock.Object);
    }

    [Fact]
    public async Task Given_EntityHasDomainEvents_When_Saving_Then_DispatchDomainEventsOnEntity()
    {
        var dbContext = new FakeEntityDbContext();

        var entity = new FakeEntity { Name = "fake" };
        var @event = new FakeEvent { Greeting = "Hello World" };

        entity.AddDomainEvent(@event);

        dbContext.FakeEntities.Add(entity);

        await _interceptor.DispatchDomainEvents(dbContext);

        _mediatorMock.Verify(x => x.Publish(It.IsAny<BaseEvent>(), It.IsAny<CancellationToken>()), Times.Once);

        entity.DomainEvents.Should().HaveCount(0);
    }
}
