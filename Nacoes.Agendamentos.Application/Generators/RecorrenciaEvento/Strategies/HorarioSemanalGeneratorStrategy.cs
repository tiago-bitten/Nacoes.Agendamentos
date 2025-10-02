using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Eventos;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Generators.RecorrenciaEvento.Strategies;

internal sealed class HorarioSemanalGeneratorStrategy : IHorarioGeneratorStrategy
{
    private const int QuantidadeDiasSemana = 7;
    
    public Result<Horario> GenerateAsync(Evento eventoMaster, DateTimeOffset dataInicioAnterior)
    {
        var dataInicial = dataInicioAnterior.AddDays(eventoMaster.Recorrencia.Intervalo!.Value * QuantidadeDiasSemana);
        
        var dataFinal = dataInicial.AddSeconds(eventoMaster.Horario.DuracaoEmSegundos);
        
        return new Horario(dataInicial, dataFinal);    
    }
}