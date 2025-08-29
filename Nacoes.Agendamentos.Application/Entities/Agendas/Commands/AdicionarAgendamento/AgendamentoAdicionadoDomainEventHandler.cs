using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Agendas.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgendamento;

internal sealed class AgendamentoAdicionadoDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<AgendamentoAdicionadoDomainEvent>
{
    public Task Handle(AgendamentoAdicionadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.AgendamentoId, acao: "Agendamento adicionado.");
    }
}