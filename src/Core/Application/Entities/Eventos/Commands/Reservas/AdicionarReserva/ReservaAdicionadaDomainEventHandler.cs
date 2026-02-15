using Domain.Shared.Events;
using Domain.Eventos.DomainEvents;
using Domain.Historicos.Interfaces;

namespace Application.Entities.Eventos.Commands.Reservas.AdicionarReserva;

internal sealed class ReservationAddedDomainEventHandler(IAuditLogRegister auditLogRegister)
    : IDomainEventHandler<ReservationAddedDomainEvent>
{
    public Task Handle(ReservationAddedDomainEvent domainEvent, CancellationToken ct)
    {
        return auditLogRegister.AuditAsync(domainEvent.ReservationId, action: "Reservation added.");
    }
}
