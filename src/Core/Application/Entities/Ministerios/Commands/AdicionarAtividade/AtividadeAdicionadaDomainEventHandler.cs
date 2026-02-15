using Domain.Shared.Events;
using Domain.Historicos;
using Domain.Historicos.Interfaces;
using Domain.Ministerios.DomainEvents;

namespace Application.Entities.Ministerios.Commands.AdicionarAtividade;

internal sealed class ActivityAddedDomainEventHandler(IAuditLogRegister auditLogRegister)
    : IDomainEventHandler<ActivityAddedDomainEvent>
{
    public Task Handle(ActivityAddedDomainEvent domainEvent, CancellationToken ct)
    {
        return auditLogRegister.AuditAsync(domainEvent.ActivityId, action: "Activity added.");
    }
}
