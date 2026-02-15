using Microsoft.Extensions.DependencyInjection;
using Application.Generators.RecorrenciaEvento.Strategies;
using Domain.Shared.ValueObjects;

namespace Application.Generators.RecorrenciaEvento.Factories;

internal sealed class ScheduleGeneratorFactory(IServiceProvider provider)
    : IScheduleGeneratorFactory
{
    public IScheduleGeneratorStrategy Create(EEventRecurrenceType type)
        => type switch
        {
            EEventRecurrenceType.Daily => provider.GetRequiredService<DailyScheduleGeneratorStrategy>(),
            EEventRecurrenceType.Weekly => provider.GetRequiredService<WeeklyScheduleGeneratorStrategy>(),
            EEventRecurrenceType.Monthly => provider.GetRequiredService<MonthlyScheduleGeneratorStrategy>(),
            EEventRecurrenceType.Yearly => provider.GetRequiredService<YearlyScheduleGeneratorStrategy>(),
            _ => throw new NotImplementedException()
        };
}
