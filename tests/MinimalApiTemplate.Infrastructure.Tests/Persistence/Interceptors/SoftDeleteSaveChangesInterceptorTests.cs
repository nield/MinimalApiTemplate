using Microsoft.EntityFrameworkCore;
using MinimalApiTemplate.Domain.Common;
using MinimalApiTemplate.Infrastructure.Persistence.Interceptors;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence.Interceptors;

public class SoftDeleteSaveChangesInterceptorTests
{
    [Fact]
    public void Given_ExistingEntity_When_RemovingEntity_Then_SetStatusToModified()
    {
        var dbContext = new FakeEntityDbContext();

        dbContext.FakeEntities.Add(new FakeEntity { Name = "fake" });
        dbContext.SaveChanges();

        var deleteItem = dbContext.FakeEntities.Single(x => x.Name == "fake");

        dbContext.FakeEntities.Remove(deleteItem);

        SoftDeleteSaveChangesInterceptor.UpdateEntities(dbContext);

        var entry = dbContext.ChangeTracker.Entries<BaseAuditableEntity>().SingleOrDefault();

        entry.Should().NotBeNull();

        entry!.Entity.Should().NotBeNull();

        entry.Entity.IsDeleted.Should().BeTrue();
        entry.State.Should().Be(EntityState.Modified);
    }
}
