using Domain.Shared.Results;
using Domain.Eventos;
using Domain.Shared.ValueObjects;

namespace Application.Generators.RecorrenciaEvento.Strategies;

public interface IHorarioGeneratorStrategy
{
    Result<Horario> GenerateAsync(Evento eventoMaster, DateTimeOffset dataInicioAnterior);
}
