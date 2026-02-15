using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Domain.Shared.Results;
using Domain.Eventos;
using Domain.Eventos.DomainEvents;
using Domain.Shared.ValueObjects;

namespace Application.Entities.Eventos.Commands.Adicionar;

internal sealed class AddEventHandler(INacoesDbContext context)
    : ICommandHandler<AddEventCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(
        AddEventCommand command,
        CancellationToken ct)
    {
        var eventResult = Event.Create(
            command.Description,
            new Schedule(command.Schedule.StartDate, command.Schedule.EndDate),
            new EventRecurrence(command.Recurrence.Type, command.Recurrence.Interval, command.Recurrence.EndDate),
            command.MaxReservationCount);

        if (eventResult.IsFailure)
        {
            return eventResult.Error;
        }

        var @event = eventResult.Value;

        await context.Events.AddAsync(@event, ct);

        @event.Raise(new MasterEventAddedDomainEvent(@event.Id));
        await context.SaveChangesAsync(ct);

        return @event.Id;
    }
}
