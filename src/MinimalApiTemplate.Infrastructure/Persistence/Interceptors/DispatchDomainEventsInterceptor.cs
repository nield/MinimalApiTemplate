using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace MinimalApiTemplate.Infrastructure.Persistence.Interceptors;

public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IServiceScopeFactory _scopeFactory;

    public DispatchDomainEventsInterceptor(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        DispatchDomainEvents(eventData.Context!).GetAwaiter().GetResult();

        return base.SavedChanges(eventData, result);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context!, cancellationToken);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    public async Task DispatchDomainEvents(DbContext context, CancellationToken cancellationToken = default)
    {
        var entities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Count > 0)
            .Select(e => e.Entity)
            .ToList();

        if (entities.Count == 0) return;
     
        var domainEvents = new List<BaseEvent>();
        
        foreach (var entity in entities)
        {
            foreach (var domainEvent in entity.DomainEvents)
            {
                // Use the DB-generated Id now that SaveChanges has completed.
                if (domainEvent is IReplaceEntityIdOnEvent replaceEntityId)
                    replaceEntityId.Id = entity.Id;
                
                domainEvents.Add(domainEvent);
            }

            entity.ClearDomainEvents();
        }
        
        // Resolve scoped services through the application scope factory. The DbContext is pooled,
        // so its internal service provider (context.GetService) does not expose application services.
        using var scope = _scopeFactory.CreateScope();
        
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent, cancellationToken);
    }
}
