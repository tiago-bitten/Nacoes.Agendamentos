using Domain.Shared.Events;
using Domain.Historicos;
using Domain.Historicos.Interfaces;
using Domain.Ministerios.DomainEvents;

namespace Application.Entities.Ministerios.Commands.AdicionarAtividade;

internal sealed class AtividadeAdicionadaDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<AtividadeAdicionadaDomainEvent>
{
    public Task Handle(AtividadeAdicionadaDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.AtividadeId, acao: "Atividade adicionada.");
    }
}
