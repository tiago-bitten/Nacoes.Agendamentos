using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Generators.RecorrenciaEvento;
using Domain.Shared.Events;
using Domain.Eventos.DomainEvents;

namespace Application.Entities.Eventos.Commands.Adicionar;

internal sealed class MasterEventAddedDomainEventHandler(
    INacoesDbContext context,
    IEventRecurrenceManager eventRecurrenceManager)
    : IDomainEventHandler<MasterEventAddedDomainEvent>
{
    public async Task Handle(MasterEventAddedDomainEvent domainEvent, CancellationToken ct)
    {
        var @event = await context.Events
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == domainEvent.EventId, ct);

        if (@event is null)
        {
            return;
        }

        await eventRecurrenceManager.GenerateInstancesAsync(@event, ct);
    }
}
