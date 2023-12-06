using System.Reflection;

namespace MinimalApiTemplate.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public static readonly string DbSchema = "template";

    public static readonly string MigrationTableName = "__EFMigrationsHistory";

    #region DbSets

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    #endregion

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasDefaultSchema(DbSchema);

        base.OnModelCreating(modelBuilder);
    }
}
