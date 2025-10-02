using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Eventos;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Generators.RecorrenciaEvento.Strategies;

public interface IHorarioGeneratorStrategy
{
    Result<Horario> GenerateAsync(Evento eventoMaster, DateTimeOffset dataInicioAnterior);
}