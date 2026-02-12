using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Generators.RecorrenciaEvento;
using Domain.Shared.Events;
using Domain.Eventos.DomainEvents;

namespace Application.Entities.Eventos.Commands.Adicionar;

public sealed class EventoMasterAdicionadoDomainEventHandler(
    INacoesDbContext context,
    IRecorrenciaEventoManager recorrenciaEventoManager)
    : IDomainEventHandler<EventoMasterAdicionadoDomainEvent>
{
    public async Task Handle(EventoMasterAdicionadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var evento = await context.Eventos
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == domainEvent.EventoId, cancellationToken);

        if (evento is null)
        {
            Console.WriteLine("Evento não encontrado.");
            return;
        }

        await recorrenciaEventoManager.GenerateInstancesAsync(evento, cancellationToken);
    }
}
