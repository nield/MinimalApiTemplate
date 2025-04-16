using MinimalApiTemplate.Domain.Common;
using MinimalApiTemplate.Infrastructure.Persistence.Interceptors;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence.Interceptors;

public class DispatchDomainEventsInterceptorTests : BaseTestFixture
{
    private readonly DispatchDomainEventsInterceptor _interceptor;

    public DispatchDomainEventsInterceptorTests()
    {
        _interceptor = new(_mediatorMock);
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

        await _mediatorMock.Received()
            .Publish(Arg.Any<BaseEvent>(), Arg.Any<CancellationToken>());

        entity.DomainEvents.Should().HaveCount(0);
    }
}
