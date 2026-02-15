using Domain.Shared.Events;
using Domain.Historicos;
using Domain.Historicos.Interfaces;
using Domain.Voluntarios.DomainEvents;

namespace Application.Entities.Voluntarios.Commands.Adicionar;

internal sealed class VolunteerAddedDomainEventHandler(IAuditLogRegister auditLogRegister)
    : IDomainEventHandler<VolunteerAddedDomainEvent>
{
    public Task Handle(VolunteerAddedDomainEvent domainEvent, CancellationToken ct)
    {
        return auditLogRegister.AuditAsync(domainEvent.VolunteerId, action: "Volunteer added.");
    }
}
