using Domain.Shared.Events;
using Domain.Historicos.Interfaces;
using Domain.Voluntarios.DomainEvents;

namespace Application.Entities.Voluntarios.Commands.Desvincular;

internal sealed class VolunteerMinistryUnlinkedDomainEventHandler(IAuditLogRegister auditLogRegister)
    : IDomainEventHandler<VolunteerMinistryUnlinkedDomainEvent>
{
    public Task Handle(
        VolunteerMinistryUnlinkedDomainEvent domainEvent,
        CancellationToken ct)
    {
        return auditLogRegister.AuditAsync(
            domainEvent.VolunteerId,
            action: $"Volunteer unlinked from ministry {domainEvent.MinistryName}.");
    }
}
