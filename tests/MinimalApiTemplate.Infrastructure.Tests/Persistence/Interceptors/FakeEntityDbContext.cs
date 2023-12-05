using Microsoft.EntityFrameworkCore;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence.Interceptors;

/// <summary>
/// Test context without any hooked-up interceptors to enable interceptor testing
/// </summary>
public class FakeEntityDbContext : DbContext
{
    public DbSet<FakeEntity> FakeEntities { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        base.OnConfiguring(optionsBuilder);
    }
}
