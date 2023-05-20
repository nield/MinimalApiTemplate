using MinimalApiTemplate.Domain.Common;
using MinimalApiTemplate.Infrastructure.Persistence.Interceptors;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence.Interceptors;

public class AuditableEntitySaveChangesInterceptorTests
{
    private readonly AuditableEntitySaveChangesInterceptor _interceptor;
    private readonly Mock<ICurrentUserService> _userServiceMock = new();

    public AuditableEntitySaveChangesInterceptorTests()
    {
        _interceptor = new(_userServiceMock.Object);
    }

    [Fact]
    public void Given_EntityIsAdded_When_UpdatingEntities_Then_SetAuditPropertiesOnEntity()
    {
        var userId = "1";

        _userServiceMock.SetupGet(x => x.UserId).Returns(userId);

        var dbContext = new FakeEntityDbContext();

        dbContext.FakeEntities.Add(new FakeEntity { Name = "test1" });

        _interceptor.UpdateEntities(dbContext);

        var entry = dbContext.ChangeTracker.Entries<BaseAuditableEntity>().SingleOrDefault();

        entry.Should().NotBeNull();

        entry!.Entity.Should().NotBeNull();

        entry.Entity.CreatedBy.Should().Be(userId);
        entry.Entity.CreatedDateTime.Should().NotBe(DateTimeOffset.MinValue);
        entry.Entity.LastModifiedBy.Should().Be(userId);
        entry.Entity.LastModifiedDateTime.Should().NotBe(DateTimeOffset.MinValue);
    }

    [Fact]
    public void Given_EntityIsModified_When_UpdatingEntities_Then_SetAuditPropertiesOnEntity()
    {
        var userId = "1";

        _userServiceMock.SetupGet(x => x.UserId).Returns(userId);

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
        entry.Entity.LastModifiedDateTime.Should().NotBe(DateTimeOffset.MinValue);
    }
}
