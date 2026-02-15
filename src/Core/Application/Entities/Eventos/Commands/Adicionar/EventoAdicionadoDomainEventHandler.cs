using Domain.Shared.Events;
using Domain.Eventos.DomainEvents;
using Domain.Historicos.Interfaces;

namespace Application.Entities.Eventos.Commands.Adicionar;

internal sealed class EventAddedDomainEventHandler(IAuditLogRegister auditLogRegister)
    : IDomainEventHandler<EventAddedDomainEvent>
{
    public Task Handle(EventAddedDomainEvent domainEvent, CancellationToken ct)
    {
        return auditLogRegister.AuditAsync(domainEvent.EventId, action: "Event added.");
    }
}
