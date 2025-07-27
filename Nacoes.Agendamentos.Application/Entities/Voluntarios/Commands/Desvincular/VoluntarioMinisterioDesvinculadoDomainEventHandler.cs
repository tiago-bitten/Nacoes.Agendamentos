using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.DomainEvents;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Desvincular;

internal sealed class VoluntarioMinisterioDesvinculadoDomainEventHandler(IHistoricoRegister historicoRegister) 
    : IDomainEventHandler<VoluntarioMinisterioDesvinculadoDomainEvent>
{
    public Task Handle(VoluntarioMinisterioDesvinculadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.VoluntarioId, acao: $"Voluntário desvinculado ao ministério {domainEvent.NomeMinisterio}.");
    }
}