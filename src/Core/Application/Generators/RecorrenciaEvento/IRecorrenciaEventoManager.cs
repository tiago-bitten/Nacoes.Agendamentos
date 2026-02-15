using Domain.Eventos;

namespace Application.Generators.RecorrenciaEvento;

public interface IEventRecurrenceManager
{
    Task GenerateInstancesAsync(Event masterEvent, CancellationToken ct = default);
    Task UpdateInstancesAsync(Event modifiedEvent, CancellationToken ct = default);
}
