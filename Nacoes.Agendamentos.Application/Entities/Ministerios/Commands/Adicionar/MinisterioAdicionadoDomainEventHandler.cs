using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.DomainEvents;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.Adicionar;

internal sealed class MinisterioAdicionadoDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<MinisterioAdicionadoDomainEvent>
{
    public Task Handle(MinisterioAdicionadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.MinisterioId, acao: "Ministerio adicionado.", EHistoricoTipoAcao.Criar);
    }
}