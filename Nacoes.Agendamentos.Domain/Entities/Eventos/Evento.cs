using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Eventos.Suspensoes;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Eventos;

public sealed class Evento : EntityId, IAggregateRoot
{
    private readonly List<Agendamento> _agendamentos = [];
    private readonly List<EventoSuspensao> _suspensoes = [];

    private Evento() { }

    private Evento(string descricao, Horario horario, EStatusEvento status, RecorrenciaEvento recorrencia)
    {
        Descricao = descricao;
        Horario = horario;
        Status = status;
        Recorrencia = recorrencia;
    }

    public string Descricao { get; private set; } = string.Empty;
    public Horario Horario { get; private set; } = null!;
    public EStatusEvento Status { get; private set; }
    public RecorrenciaEvento Recorrencia { get; private set; } = null!;

    public IReadOnlyCollection<Agendamento> Agendamentos => _agendamentos.AsReadOnly();
    public IReadOnlyCollection<EventoSuspensao> Suspensoes => _suspensoes.AsReadOnly();
    
    public static Result<Evento> Criar(string descricao, Horario horario, RecorrenciaEvento recorrencia)
    {
        if (string.IsNullOrWhiteSpace(descricao))
        {
            return EventoErrors.DescricaoObrigatoria;
        }

        return new Evento(descricao, horario, EStatusEvento.Aberto, recorrencia);
    }

    public Result Suspender(DateOnly? dataEncerramento)
    {
        if (Status is not (EStatusEvento.Aberto or EStatusEvento.Lotado))
        {
            return EventoErrors.NaoEstaDisponivelParaSuspender;
        }

        var suspencaoResult = EventoSuspensao.Criar(dataEncerramento);
        if (suspencaoResult.IsFailure)
        {
            return suspencaoResult.Error;
        }

        var suspencao = suspencaoResult.Value;
        _suspensoes.Add(suspencao);

        return Result.Success();
    }

    public Result<Agendamento> CriarAgendamento(Guid voluntarioMinisterioId, Guid atividadeId, EOrigemAgendamento origem)
    {
        var agendamentoResult = Agendamento.Criar(voluntarioMinisterioId, atividadeId, origem);
        if (agendamentoResult.IsFailure)
        {
            return agendamentoResult.Error;
        }

        var agendamento = agendamentoResult.Value;
        _agendamentos.Add(agendamento);

        return agendamento;
    }

    public Result CancelarAgendamento(Guid agendamentoId)
    {
        if (Status is not (EStatusEvento.Aberto or EStatusEvento.Lotado or EStatusEvento.Suspenso))
        {
            return EventoErrors.NaoEstaDisponivelParaCancelarAgendamento;
        }
        
        var agendamento = _agendamentos.FirstOrDefault(a => a.Id == agendamentoId);
        if (agendamento is null)
        {
            return AgendamentoErrors.NaoEncontrado;
        }

        return agendamento.Cancelar();
    }
}

public enum EStatusEvento
{
    Aberto = 0,
    Cancelado = 1,
    Encerrado = 2,
    Lotado = 3,
    Suspenso = 4
}