using Domain.Shared.Events;

namespace Postgres.DomainEvents;

internal interface IDomainEventsDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default);
}
