using Domain.Shared.Events;
using Domain.Historicos;
using Domain.Historicos.Interfaces;
using Domain.Ministerios.DomainEvents;

namespace Application.Entities.Ministerios.Commands.Adicionar;

internal sealed class MinistryAddedDomainEventHandler(IAuditLogRegister auditLogRegister)
    : IDomainEventHandler<MinistryAddedDomainEvent>
{
    public Task Handle(MinistryAddedDomainEvent domainEvent, CancellationToken ct)
    {
        return auditLogRegister.AuditAsync(domainEvent.MinistryId, action: "Ministry added.");
    }
}
