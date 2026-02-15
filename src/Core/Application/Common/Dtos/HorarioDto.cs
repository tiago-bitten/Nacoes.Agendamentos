using Domain.Shared.ValueObjects;

namespace Application.Common.Dtos;

public record ScheduleDto(DateTimeOffset StartDate, DateTimeOffset EndDate);

public static class ScheduleDtoExtensions
{
    public static ScheduleDto ToDto(this Schedule schedule) => new(schedule.StartDate, schedule.EndDate);
}
