using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Eventos.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Eventos.Reservas;
using Nacoes.Agendamentos.Domain.Entities.Eventos.Suspensoes;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Eventos;

public sealed class Evento : EntityId, IAggregateRoot
{
    private readonly List<Reserva> _agendamentos = [];
    private readonly List<EventoSuspensao> _suspensoes = [];

    private Evento() { }

    private Evento(string descricao, Horario horario, EStatusEvento status, RecorrenciaEvento recorrencia, int? quantidadeMaximaReservas)
    {
        Descricao = descricao;
        Horario = horario;
        Status = status;
        Recorrencia = recorrencia;
        QuantidadeMaximaReservas = quantidadeMaximaReservas;
    }

    public string Descricao { get; private set; } = string.Empty;
    public Horario Horario { get; private set; } = null!;
    public int QuantidadeReservas { get; private set; }
    public int? QuantidadeMaximaReservas { get; private set; }
    public EStatusEvento Status { get; private set; }
    public RecorrenciaEvento Recorrencia { get; private set; } = null!;

    public IReadOnlyCollection<Reserva> Agendamentos => _agendamentos.AsReadOnly();
    public IReadOnlyCollection<EventoSuspensao> Suspensoes => _suspensoes.AsReadOnly();
    
    public static Result<Evento> Criar(string descricao, Horario horario, RecorrenciaEvento recorrencia, int? quantidadeMaximaReservas)
    {
        if (string.IsNullOrWhiteSpace(descricao))
        {
            return EventoErrors.DescricaoObrigatoria;
        }
        
        if (quantidadeMaximaReservas is < 1)
        {
            return EventoErrors.QuantidadeMaximaReservasInvalida;
        }

        var evento = new Evento(descricao, horario, EStatusEvento.Aberto, recorrencia, quantidadeMaximaReservas);
        
        evento.Raise(new EventoAdicionadoDomainEvent(evento.Id));

        return evento;
    }

    public Result AtualizarHorario(Horario horario)
    {
        if (Status is not (EStatusEvento.Aberto or EStatusEvento.Lotado or EStatusEvento.Suspenso))
        {
            return EventoErrors.NaoEstaDisponivelParaAtualizarHorario;
        }
        
        Horario = horario;
        
        return Result.Success();
    }
    
    public Result AtualizarRecorrencia(RecorrenciaEvento recorrencia)
    {
        if (Status is not (EStatusEvento.Aberto or EStatusEvento.Lotado or EStatusEvento.Suspenso))
        {
            return EventoErrors.NaoEstaDisponivelParaAtualizarRecorrencia;
        }
        
        Recorrencia = recorrencia;
        
        return Result.Success();
    }

    public Result Cancelar()
    {
        if (Status is not (EStatusEvento.Aberto or EStatusEvento.Lotado or EStatusEvento.Suspenso))
        {
            return EventoErrors.NaoEstaDisponivelParaCancelar;
        }
        
        var agendamentosParaCancelar = _agendamentos
            .Where(x => x.Status is EStatusReserva.Confirmado or EStatusReserva.AguardandoConfirmacao);
        
        foreach (var agendamento in agendamentosParaCancelar)
        {
            var agendamentoCanceladoResult = agendamento.Cancelar();
            if (agendamentoCanceladoResult.IsFailure)
            {
                return agendamentoCanceladoResult.Error;
            }
        }
        
        Status = EStatusEvento.Cancelado;
        
        return Result.Success();
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

    public Result<Reserva> CriarAgendamento(Guid voluntarioMinisterioId, Guid atividadeId, EOrigemReserva origem)
    {
        var agendamentoResult = Reserva.Criar(voluntarioMinisterioId, atividadeId, origem);
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
            return ReservaErrors.NaoEncontrado;
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