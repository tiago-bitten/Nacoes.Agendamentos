using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Generators.RecorrenciaEvento;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Eventos.DomainEvents;

namespace Nacoes.Agendamentos.Application.Entities.Eventos.Commands.Adicionar;

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