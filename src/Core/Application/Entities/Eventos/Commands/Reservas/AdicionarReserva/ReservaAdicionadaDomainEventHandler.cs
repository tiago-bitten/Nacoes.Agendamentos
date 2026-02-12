using Domain.Shared.Events;
using Domain.Eventos.DomainEvents;
using Domain.Historicos.Interfaces;

namespace Application.Entities.Eventos.Commands.AdicionarAgendamento;

internal sealed class ReservaAdicionadaDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<AgendamentoAdicionadoDomainEvent>
{
    public Task Handle(AgendamentoAdicionadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.AgendamentoId, acao: "Agendamento adicionado.");
    }
}
