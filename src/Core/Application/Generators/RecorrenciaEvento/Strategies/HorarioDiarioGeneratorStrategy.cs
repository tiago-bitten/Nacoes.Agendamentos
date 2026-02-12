using Domain.Shared.Results;
using Domain.Eventos;
using Domain.Shared.ValueObjects;

namespace Application.Generators.RecorrenciaEvento.Strategies;

internal sealed class HorarioDiarioGeneratorStrategy : IHorarioGeneratorStrategy
{
    public Result<Horario> GenerateAsync(Evento eventoMaster, DateTimeOffset dataInicioAnterior)
    {
        var dataInicial = dataInicioAnterior.AddDays(eventoMaster.Recorrencia.Intervalo!.Value);

        var dataFinal = dataInicial.AddSeconds(eventoMaster.Horario.DuracaoEmSegundos);

        return new Horario(dataInicial, dataFinal);
    }
}
