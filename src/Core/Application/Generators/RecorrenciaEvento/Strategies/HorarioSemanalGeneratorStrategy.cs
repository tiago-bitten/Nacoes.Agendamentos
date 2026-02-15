using Domain.Shared.Results;
using Domain.Eventos;
using Domain.Shared.ValueObjects;

namespace Application.Generators.RecorrenciaEvento.Strategies;

internal sealed class WeeklyScheduleGeneratorStrategy : IScheduleGeneratorStrategy
{
    private const int DaysPerWeek = 7;

    public Result<Schedule> Generate(Event masterEvent, DateTimeOffset previousStartDate)
    {
        var startDate = previousStartDate.AddDays(masterEvent.Recurrence.Interval!.Value * DaysPerWeek);

        var endDate = startDate.AddSeconds(masterEvent.Schedule.DurationInSeconds);

        return new Schedule(startDate, endDate);
    }
}
