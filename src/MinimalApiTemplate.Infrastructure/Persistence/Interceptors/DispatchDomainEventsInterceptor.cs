using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MinimalApiTemplate.Infrastructure.Persistence.Interceptors;

public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IMediator _mediator;

    public DispatchDomainEventsInterceptor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        _mediator.DispatchDomainEvents(eventData.Context!).GetAwaiter().GetResult();

        return base.SavedChanges(eventData, result);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(eventData.Context!);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}
