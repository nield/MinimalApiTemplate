using MinimalApiTemplate.Infrastructure.Persistence;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence;

[Collection("PersistenceCollection")]
public abstract class BasePersistenceTestFixture<T>
    : BaseTestFixture<T> where T : class
{
    protected ApplicationDbContext _dbContext;

    protected BasePersistenceTestFixture(PersistenceFixture persistenceFixture)
    {
        _dbContext = persistenceFixture.InMemoryApplicationDbContextFactory.CreateContext(Guid.NewGuid().ToString());
    }
}
