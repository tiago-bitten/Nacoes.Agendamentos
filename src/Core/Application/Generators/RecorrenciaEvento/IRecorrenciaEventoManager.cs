using Domain.Eventos;

namespace Application.Generators.RecorrenciaEvento;

public interface IRecorrenciaEventoManager
{
    Task GenerateInstancesAsync(Evento eventoMaster, CancellationToken cancellationToken = default);
    Task UpdateInstancesAsync(Evento eventoAlterado, CancellationToken cancellationToken = default);
}
