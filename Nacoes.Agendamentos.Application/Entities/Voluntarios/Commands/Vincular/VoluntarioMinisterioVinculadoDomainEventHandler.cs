using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.DomainEvents;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Vincular;

internal sealed class VoluntarioMinisterioVinculadoDomainEventHandler(IHistoricoRegister historicoRegister) 
    : IDomainEventHandler<VoluntarioMinisterioVinculadoDomainEvent>
{
    public Task Handle(VoluntarioMinisterioVinculadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.VoluntarioId, acao: $"Voluntário vinculado ao ministério {domainEvent.NomeMinisterio}.");
    }
}