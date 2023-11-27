using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MinimalApiTemplate.Infrastructure.Persistence.Interceptors;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _dateTimeProvider;

    public AuditableEntitySaveChangesInterceptor(
        ICurrentUserService currentUserService,
        TimeProvider dateTimeProvider)
    {
        _currentUserService = currentUserService;
        _dateTimeProvider = dateTimeProvider;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _currentUserService.UserId;
                entry.Entity.CreatedDateTime = _dateTimeProvider.GetUtcNow();
            }

            if (entry.State == EntityState.Added
                    || entry.State == EntityState.Modified
                    || entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastModifiedBy = _currentUserService.UserId;
                entry.Entity.LastModifiedDateTime = _dateTimeProvider.GetUtcNow();
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added
                || r.TargetEntry.State == EntityState.Modified));
}
