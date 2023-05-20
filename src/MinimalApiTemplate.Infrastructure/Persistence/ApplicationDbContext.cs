using System.Reflection;
using MinimalApiTemplate.Infrastructure.Persistence.Interceptors;
using MediatR;

namespace MinimalApiTemplate.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public static readonly string DbSchema = "template";

    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    private static readonly SoftDeleteSaveChangesInterceptor SoftDeleteSaveChangesInterceptor = new();

    #region DbSets

    public DbSet<TodoItem> TodoItems => Set<TodoItem>(); 

    #endregion

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasDefaultSchema(DbSchema);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(SoftDeleteSaveChangesInterceptor,
                                        _auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var changes = await base.SaveChangesAsync(cancellationToken);

        await _mediator.DispatchDomainEvents(this);

        return changes;
    }
}
