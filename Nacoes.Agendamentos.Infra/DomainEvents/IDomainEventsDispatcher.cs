using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Infra.DomainEvents;

public interface IDomainEventsDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}
