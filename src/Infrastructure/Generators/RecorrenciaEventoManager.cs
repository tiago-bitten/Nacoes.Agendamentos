using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Generators.RecorrenciaEvento;
using Application.Generators.RecorrenciaEvento.Factories;
using Domain.Shared.Results;
using Domain.Eventos;
using Domain.Shared.ValueObjects;

namespace Infrastructure.Generators;

internal sealed class RecorrenciaEventoManager(
    INacoesDbContext context,
    IHorarioGeneratorFactory horarioGeneratorFactory)
    : IRecorrenciaEventoManager
{
    public const int QuantidadeMaximaDias = 365;

    public async Task GenerateInstancesAsync(Evento eventoMaster, CancellationToken cancellationToken = default)
    {
        if (eventoMaster.Recorrencia.Tipo is ETipoRecorrenciaEvento.Nenhuma)
        {
            return;
        }

        var horarioGenerator = horarioGeneratorFactory.Create(eventoMaster.Recorrencia.Tipo);

        var dataFinal = DateOnly.FromDateTime(DateTimeOffset.UtcNow.DateTime).AddDays(QuantidadeMaximaDias);

        if (eventoMaster.Recorrencia.DataFinal < dataFinal)
        {
            dataFinal = eventoMaster.Recorrencia.DataFinal.Value;
        }

        var dataInicio = eventoMaster.Horario.DataInicial;

        var horarios = new List<Horario>();
        while (DateOnly.FromDateTime(dataInicio.DateTime) <= dataFinal)
        {
            var horarioResult = horarioGenerator.GenerateAsync(eventoMaster, dataInicio);
            if (horarioResult.IsFailure)
            {
                continue;
            }

            var horario = horarioResult.Value;
            horarios.Add(horario);

            dataInicio = horario.DataInicial;
        }

        foreach (var horario in horarios)
        {
            var eventoInstanceResult = Evento.Criar(eventoMaster.Descricao, horario, eventoMaster.Recorrencia.Copiar(), eventoMaster.QuantidadeMaximaReservas);
            if (eventoInstanceResult.IsFailure)
            {
                continue;
            }

            var eventoInstance = eventoInstanceResult.Value;

            await context.Eventos.AddAsync(eventoInstance, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateInstancesAsync(Evento eventoAlterado, CancellationToken cancellationToken = default)
    {
        var statusEventosDisponivel = new[]
        {
            EStatusEvento.Aberto,
            EStatusEvento.Lotado,
            EStatusEvento.Suspenso
        };

        var eventos = await context.Eventos
            .Where(x => x.Recorrencia.Id == eventoAlterado.Recorrencia.Id &&
                        statusEventosDisponivel.Contains(x.Status) &&
                        x.Horario.DataInicial > eventoAlterado.Horario.DataInicial &&
                        x.Id != eventoAlterado.Id)
            .ToListAsync(cancellationToken);


        var horarioAtualizado = eventoAlterado.Horario;
        var recorrenciaAtualizada = eventoAlterado.Recorrencia;

        var erros = new List<Error>();

        foreach (var evento in eventos)
        {
            var atualizarHorarioResult = evento.AtualizarHorario(horarioAtualizado);
            var atualizarRecorrenciaResult = evento.AtualizarRecorrencia(recorrenciaAtualizada);

            if (atualizarHorarioResult.IsFailure)
            {
                erros.Add(atualizarHorarioResult.Error);
            }

            if (atualizarRecorrenciaResult.IsFailure)
            {
                erros.Add(atualizarRecorrenciaResult.Error);
            }
        }

        if (erros.Count > 0)
        {
            // WarningContext
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
