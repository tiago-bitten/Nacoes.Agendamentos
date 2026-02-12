using Domain.Shared.Events;
using Domain.Historicos.Interfaces;
using Domain.Voluntarios.DomainEvents;

namespace Application.Entities.Voluntarios.Commands.Vincular;

internal sealed class VoluntarioMinisterioVinculadoDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<VoluntarioMinisterioVinculadoDomainEvent>
{
    public Task Handle(VoluntarioMinisterioVinculadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.VoluntarioId, acao: $"Voluntário vinculado ao ministério {domainEvent.NomeMinisterio}.");
    }
}
