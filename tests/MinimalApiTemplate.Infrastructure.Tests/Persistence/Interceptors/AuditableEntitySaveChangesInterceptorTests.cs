using MinimalApiTemplate.Domain.Common;
using MinimalApiTemplate.Infrastructure.Persistence.Interceptors;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence.Interceptors;

public class AuditableEntitySaveChangesInterceptorTests
{
    private readonly AuditableEntitySaveChangesInterceptor _interceptor;
    private readonly Mock<ICurrentUserService> _userServiceMock = new();
    private readonly Mock<TimeProvider> _timeProviderMock = new();

    public AuditableEntitySaveChangesInterceptorTests()
    {
        _interceptor = new(_userServiceMock.Object, _timeProviderMock.Object);
    }

    [Fact]
    public void Given_EntityIsAdded_When_UpdatingEntities_Then_SetAuditPropertiesOnEntity()
    {
        var userId = "1";
        var dateTime = new DateTimeOffset(2023, 12, 1, 1, 0, 0, TimeSpan.Zero);

        _userServiceMock.SetupGet(x => x.UserId).Returns(userId);

        _timeProviderMock.Setup(x => x.GetUtcNow()).Returns(dateTime);

        var dbContext = new FakeEntityDbContext();

        dbContext.FakeEntities.Add(new FakeEntity { Name = "test1" });

        _interceptor.UpdateEntities(dbContext);

        var entry = dbContext.ChangeTracker.Entries<BaseAuditableEntity>().SingleOrDefault();

        entry.Should().NotBeNull();

        entry!.Entity.Should().NotBeNull();

        entry.Entity.CreatedBy.Should().Be(userId);
        entry.Entity.CreatedDateTime.Should().Be(dateTime);
        entry.Entity.LastModifiedBy.Should().Be(userId);
        entry.Entity.LastModifiedDateTime.Should().Be(dateTime);
    }

    [Fact]
    public void Given_EntityIsModified_When_UpdatingEntities_Then_SetAuditPropertiesOnEntity()
    {
        var userId = "1";
        var dateTime = new DateTimeOffset(2023, 12, 1, 1, 0, 0, TimeSpan.Zero);

        _userServiceMock.SetupGet(x => x.UserId).Returns(userId);
       
        _timeProviderMock.Setup(x => x.GetUtcNow()).Returns(dateTime);
       
        var dbContext = new FakeEntityDbContext();
        dbContext.FakeEntities.Add(new FakeEntity { Name = "update1" });
        dbContext.SaveChanges();

        var updateItem = dbContext.FakeEntities.Single(x => x.Name == "update1");

        updateItem.Name = "updated";

        _interceptor.UpdateEntities(dbContext);

        var entry = dbContext.ChangeTracker.Entries<BaseAuditableEntity>().SingleOrDefault();

        entry.Should().NotBeNull();

        entry!.Entity.Should().NotBeNull();

        entry.Entity.LastModifiedBy.Should().Be(userId);
        entry.Entity.LastModifiedDateTime.Should().Be(dateTime);
    }
}
