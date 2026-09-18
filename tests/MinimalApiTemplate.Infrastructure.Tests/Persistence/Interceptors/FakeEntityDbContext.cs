using Microsoft.EntityFrameworkCore;

namespace MinimalApiTemplate.Infrastructure.Tests.Persistence.Interceptors;

/// <summary>
/// Test context without any hooked-up interceptors to enable interceptor testing
/// </summary>
public class FakeEntityDbContext : DbContext
{
    private readonly IServiceProvider? _applicationServiceProvider;

    public FakeEntityDbContext(IServiceProvider? applicationServiceProvider = null)
    {
        _applicationServiceProvider = applicationServiceProvider;
    }

    public DbSet<FakeEntity> FakeEntities { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        if (_applicationServiceProvider is not null)
        {
            optionsBuilder.UseApplicationServiceProvider(_applicationServiceProvider);
        }

        base.OnConfiguring(optionsBuilder);
    }
}
