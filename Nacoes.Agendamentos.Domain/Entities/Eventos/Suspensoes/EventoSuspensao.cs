using System.Diagnostics.CodeAnalysis;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Eventos.Suspensoes;

public sealed class EventoSuspensao : EntityId
{
    private EventoSuspensao() { }
    
    private EventoSuspensao(DateOnly? dataEncerramento, EStatusEventoSuspensao status)
    {
        DataEncerramento = dataEncerramento;
        Status = status;
    }
    
    public Guid EventoId { get; private set; }
    public DateOnly? DataEncerramento { get; private set; }
    public DateTimeOffset? DataConclusaoEncerramento { get; private set; }
    public EStatusEventoSuspensao Status { get; private set; }
    
    internal static Result<EventoSuspensao> Criar(DateOnly? dataFinal)
    {
        var dataHoje = DateOnly.FromDateTime(DateTimeOffset.UtcNow.DateTime);
        
        if (dataFinal == dataHoje)
        { 
            return EventoSuspensaoErrors.DataFinalNaoPodeSerHoje;
        }
        
        if (dataFinal < dataHoje)
        {
            return EventoSuspensaoErrors.DataFinalNaoPodeSerAnterioraDataHoje;
        }
        
        return new EventoSuspensao(dataFinal, EStatusEventoSuspensao.Ativo);
    }
    
    internal Result Cancelar()
    {
        if (Status is EStatusEventoSuspensao.Cancelado)
        {
            return EventoSuspensaoErrors.NaoEstaDisponivelParaCancelar;
        }
        
        Status = EStatusEventoSuspensao.Cancelado;
        
        return Result.Success();
    }

    internal Result Encerrar()
    {
        if (Status is not EStatusEventoSuspensao.Ativo)
        {
            return EventoSuspensaoErrors.NaoEstaDisponivelParaEncerrar;
        }
        
        Status = EStatusEventoSuspensao.Encerrado;
        DataConclusaoEncerramento = DateTimeOffset.UtcNow;
        
        return Result.Success();
    }
}

public enum EStatusEventoSuspensao
{
    Ativo = 0,
    Cancelado = 1,
    Encerrado = 2
}