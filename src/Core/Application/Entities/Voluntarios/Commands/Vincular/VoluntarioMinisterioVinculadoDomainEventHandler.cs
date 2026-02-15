using Domain.Shared.Events;
using Domain.Historicos.Interfaces;
using Domain.Voluntarios.DomainEvents;

namespace Application.Entities.Voluntarios.Commands.Vincular;

internal sealed class VolunteerMinistryLinkedDomainEventHandler(IAuditLogRegister auditLogRegister)
    : IDomainEventHandler<VolunteerMinistryLinkedDomainEvent>
{
    public Task Handle(
        VolunteerMinistryLinkedDomainEvent domainEvent,
        CancellationToken ct)
    {
        return auditLogRegister.AuditAsync(
            domainEvent.VolunteerId,
            action: $"Volunteer linked to ministry {domainEvent.MinistryName}.");
    }
}
