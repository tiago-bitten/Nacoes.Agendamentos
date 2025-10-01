using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Eventos.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Eventos.Commands.Adicionar;

internal sealed class EventoAdicionadoDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<EventoAdicionadoDomainEvent>
{
    public Task Handle(EventoAdicionadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.EventoId, acao: "Evento adicionado.");
    }
}