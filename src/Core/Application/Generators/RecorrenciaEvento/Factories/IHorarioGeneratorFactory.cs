using Application.Generators.RecorrenciaEvento.Strategies;
using Domain.Shared.ValueObjects;

namespace Application.Generators.RecorrenciaEvento.Factories;

public interface IScheduleGeneratorFactory
{
    IScheduleGeneratorStrategy Create(EEventRecurrenceType type);
}
