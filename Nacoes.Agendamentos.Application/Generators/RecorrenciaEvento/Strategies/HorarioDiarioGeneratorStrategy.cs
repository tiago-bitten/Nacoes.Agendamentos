﻿using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Eventos;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Generators.RecorrenciaEvento.Strategies;

internal sealed class HorarioDiarioGeneratorStrategy : IHorarioGeneratorStrategy
{
    public Result<Horario> GenerateAsync(Evento eventoMaster, DateTimeOffset dataInicioAnterior)
    {
        var dataInicial = dataInicioAnterior.AddDays(eventoMaster.Recorrencia.Intervalo!.Value);
        
        var dataFinal = dataInicial.AddSeconds(eventoMaster.Horario.DuracaoEmSegundos);
        
        return new Horario(dataInicial, dataFinal);
    }
}
