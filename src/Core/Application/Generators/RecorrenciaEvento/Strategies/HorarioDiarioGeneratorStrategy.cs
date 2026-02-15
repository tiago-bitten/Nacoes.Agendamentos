using Domain.Shared.Results;
using Domain.Eventos;
using Domain.Shared.ValueObjects;

namespace Application.Generators.RecorrenciaEvento.Strategies;

internal sealed class DailyScheduleGeneratorStrategy : IScheduleGeneratorStrategy
{
    public Result<Schedule> Generate(Event masterEvent, DateTimeOffset previousStartDate)
    {
        var startDate = previousStartDate.AddDays(masterEvent.Recurrence.Interval!.Value);

        var endDate = startDate.AddSeconds(masterEvent.Schedule.DurationInSeconds);

        return new Schedule(startDate, endDate);
    }
}
