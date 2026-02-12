using Domain.Shared.Events;
using Domain.Historicos.Interfaces;
using Domain.Voluntarios.DomainEvents;

namespace Application.Entities.Voluntarios.Commands.Desvincular;

internal sealed class VoluntarioMinisterioDesvinculadoDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<VoluntarioMinisterioDesvinculadoDomainEvent>
{
    public Task Handle(VoluntarioMinisterioDesvinculadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.VoluntarioId, acao: $"Voluntário desvinculado ao ministério {domainEvent.NomeMinisterio}.");
    }
}
