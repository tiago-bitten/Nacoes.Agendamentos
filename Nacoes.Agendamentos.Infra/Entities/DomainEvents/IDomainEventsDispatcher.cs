using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Infra.Entities.DomainEvents;

public interface IDomainEventsDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}
