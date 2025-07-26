using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Adicionar;

internal sealed class AgendaAdicionadaDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<AgendaAdicionadaDomainEvent>
{
    public Task Handle(AgendaAdicionadaDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.AgendaId, acao: "Agenda adicionada.", EHistoricoTipoAcao.Criar);
    }
}