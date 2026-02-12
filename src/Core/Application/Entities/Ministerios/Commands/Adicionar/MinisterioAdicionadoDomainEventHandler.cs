using Domain.Shared.Events;
using Domain.Historicos;
using Domain.Historicos.Interfaces;
using Domain.Ministerios.DomainEvents;

namespace Application.Entities.Ministerios.Commands.Adicionar;

internal sealed class MinisterioAdicionadoDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<MinisterioAdicionadoDomainEvent>
{
    public Task Handle(MinisterioAdicionadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.MinisterioId, acao: "Ministerio adicionado.");
    }
}
