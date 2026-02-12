using Application.Generators.RecorrenciaEvento.Strategies;
using Domain.Shared.ValueObjects;

namespace Application.Generators.RecorrenciaEvento.Factories;

public interface IHorarioGeneratorFactory
{
    IHorarioGeneratorStrategy Create(ETipoRecorrenciaEvento tipo);
}
