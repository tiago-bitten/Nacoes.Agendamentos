using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Eventos.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Eventos.Commands.AdicionarAgendamento;

internal sealed class ReservaAdicionadaDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<AgendamentoAdicionadoDomainEvent>
{
    public Task Handle(AgendamentoAdicionadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.AgendamentoId, acao: "Agendamento adicionado.");
    }
}