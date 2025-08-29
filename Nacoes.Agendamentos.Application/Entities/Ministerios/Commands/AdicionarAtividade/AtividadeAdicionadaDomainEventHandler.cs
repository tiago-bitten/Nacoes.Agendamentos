using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.DomainEvents;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;

internal sealed class AtividadeAdicionadaDomainEventHandler(IHistoricoRegister historicoRegister) 
    : IDomainEventHandler<AtividadeAdicionadaDomainEvent>
{
    public Task Handle(AtividadeAdicionadaDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.AtividadeId, acao: "Atividade adicionada.");
    }
}