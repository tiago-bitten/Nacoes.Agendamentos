using Nacoes.Agendamentos.Domain.Entities.Eventos;

namespace Nacoes.Agendamentos.Application.Generators.RecorrenciaEvento;

public interface IRecorrenciaEventoManager
{
    Task GenerateInstancesAsync(Evento eventoMaster, CancellationToken cancellationToken = default);
    Task UpdateInstancesAsync(Evento eventoAlterado, CancellationToken cancellationToken = default);
}