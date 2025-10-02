using Microsoft.Extensions.DependencyInjection;
using Nacoes.Agendamentos.Application.Generators.RecorrenciaEvento.Strategies;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Generators.RecorrenciaEvento.Factories;

internal sealed class HorarioGeneratorFactory(IServiceProvider provider)
    : IHorarioGeneratorFactory
{
    public IHorarioGeneratorStrategy Create(ETipoRecorrenciaEvento tipo)
        => tipo switch
        {
            ETipoRecorrenciaEvento.Diario => provider.GetRequiredService<HorarioDiarioGeneratorStrategy>(),
            ETipoRecorrenciaEvento.Semanal => provider.GetRequiredService<HorarioSemanalGeneratorStrategy>(),
            ETipoRecorrenciaEvento.Mensal => provider.GetRequiredService<HorarioMensalGeneratorStrategy>(),
            ETipoRecorrenciaEvento.Anual => provider.GetRequiredService<HorarioAnualGeneratorStrategy>(),
            _ => throw new NotImplementedException()
        };
}