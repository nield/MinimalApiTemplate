using Mediator;
using Microsoft.Extensions.DependencyInjection;
using MinimalApiTemplate.Domain.Common;
using MinimalApiTemplate.Infrastructure.Persistence.Interceptors;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence.Interceptors;

public class DispatchDomainEventsInterceptorTests : BaseTestFixture
{
    private readonly DispatchDomainEventsInterceptor _interceptor;
    private readonly IServiceProvider _serviceProvider;

    public DispatchDomainEventsInterceptorTests()
    {
        _serviceProvider = new ServiceCollection()
            .AddSingleton(_mediatorMock)
            .BuildServiceProvider();

        _interceptor = new DispatchDomainEventsInterceptor(
            _serviceProvider.GetRequiredService<IServiceScopeFactory>());
    }

    [Fact]
    public async Task Given_EntityHasDomainEvents_When_Saving_Then_DispatchDomainEventsOnEntity()
    {
        var dbContext = new FakeEntityDbContext(_serviceProvider);

        var entity = new FakeEntity { Name = "fake" };
        var @event = new FakeEvent { Greeting = "Hello World" };

        entity.AddDomainEvent(@event);

        dbContext.FakeEntities.Add(entity);

        await _interceptor.DispatchDomainEvents(dbContext, TestContext.Current.CancellationToken);

        await _mediatorMock.Received()
            .Publish(Arg.Any<BaseEvent>(), Arg.Any<CancellationToken>());

        entity.DomainEvents.Should().HaveCount(0);
    }
}
