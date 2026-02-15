using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Generators.RecorrenciaEvento;
using Application.Generators.RecorrenciaEvento.Factories;
using Domain.Shared.Results;
using Domain.Eventos;
using Domain.Shared.ValueObjects;

namespace Infrastructure.Generators;

internal sealed class EventRecurrenceManager(
    INacoesDbContext context,
    IScheduleGeneratorFactory scheduleGeneratorFactory)
    : IEventRecurrenceManager
{
    public const int MaxDays = 365;

    public async Task GenerateInstancesAsync(Event masterEvent, CancellationToken ct = default)
    {
        if (masterEvent.Recurrence.Type is EEventRecurrenceType.None)
        {
            return;
        }

        var scheduleGenerator = scheduleGeneratorFactory.Create(masterEvent.Recurrence.Type);

        var endDate = DateOnly.FromDateTime(DateTimeOffset.UtcNow.DateTime).AddDays(MaxDays);

        if (masterEvent.Recurrence.EndDate < endDate)
        {
            endDate = masterEvent.Recurrence.EndDate.Value;
        }

        var startDate = masterEvent.Schedule.StartDate;

        var schedules = new List<Schedule>();
        while (DateOnly.FromDateTime(startDate.DateTime) <= endDate)
        {
            var scheduleResult = scheduleGenerator.Generate(masterEvent, startDate);
            if (scheduleResult.IsFailure)
            {
                continue;
            }

            var schedule = scheduleResult.Value;
            schedules.Add(schedule);

            startDate = schedule.StartDate;
        }

        foreach (var schedule in schedules)
        {
            var eventInstanceResult = Event.Create(
                masterEvent.Description,
                schedule,
                masterEvent.Recurrence.Copy(),
                masterEvent.MaxReservationCount);
            if (eventInstanceResult.IsFailure)
            {
                continue;
            }

            var eventInstance = eventInstanceResult.Value;

            await context.Events.AddAsync(eventInstance, ct);
        }

        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateInstancesAsync(Event updatedEvent, CancellationToken ct = default)
    {
        var availableEventStatuses = new[]
        {
            EEventStatus.Open,
            EEventStatus.Full,
            EEventStatus.Suspended
        };

        var events = await context.Events
            .Where(x => x.Recurrence.Id == updatedEvent.Recurrence.Id &&
                        availableEventStatuses.Contains(x.Status) &&
                        x.Schedule.StartDate > updatedEvent.Schedule.StartDate &&
                        x.Id != updatedEvent.Id)
            .ToListAsync(ct);


        var updatedSchedule = updatedEvent.Schedule;
        var updatedRecurrence = updatedEvent.Recurrence;

        var errors = new List<Error>();

        foreach (var @event in events)
        {
            var updateScheduleResult = @event.UpdateSchedule(updatedSchedule);
            var updateRecurrenceResult = @event.UpdateRecurrence(updatedRecurrence);

            if (updateScheduleResult.IsFailure)
            {
                errors.Add(updateScheduleResult.Error);
            }

            if (updateRecurrenceResult.IsFailure)
            {
                errors.Add(updateRecurrenceResult.Error);
            }
        }

        if (errors.Count > 0)
        {
        }

        await context.SaveChangesAsync(ct);
    }
}
