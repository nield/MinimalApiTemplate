using MinimalApiTemplate.Infrastructure.Persistence.Interceptors;

using Microsoft.Extensions.DependencyInjection;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence;
public class PersistenceFixture
{
    private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();

    public InMemoryApplicationDbContextFactory InMemoryApplicationDbContextFactory { get; set; }

    public PersistenceFixture()
    {
        var services = new ServiceCollection();

        _currentUserServiceMock.SetupGet(x => x.UserId).Returns("1");

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<IApplicationMarker>());
        services.AddScoped<ICurrentUserService>((sp) => _currentUserServiceMock.Object);

        var serviceProvider = services.BuildServiceProvider();

        InMemoryApplicationDbContextFactory = new InMemoryApplicationDbContextFactory(serviceProvider);

        Console.WriteLine("Persistence setup done once");
    }
}