using Domain.Shared.Results;
using Domain.Eventos;
using Domain.Shared.ValueObjects;

namespace Application.Generators.RecorrenciaEvento.Strategies;

public interface IScheduleGeneratorStrategy
{
    Result<Schedule> Generate(Event masterEvent, DateTimeOffset previousStartDate);
}
