using Domain.Shared.Events;
using Domain.Eventos.DomainEvents;
using Domain.Historicos.Interfaces;

namespace Application.Entities.Eventos.Commands.Adicionar;

internal sealed class EventoAdicionadoDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<EventoAdicionadoDomainEvent>
{
    public Task Handle(EventoAdicionadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.EventoId, acao: "Evento adicionado.");
    }
}
