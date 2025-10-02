using Nacoes.Agendamentos.Application.Generators.RecorrenciaEvento.Strategies;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Generators.RecorrenciaEvento.Factories;

public interface IHorarioGeneratorFactory
{
    IHorarioGeneratorStrategy Create(ETipoRecorrenciaEvento tipo);
}