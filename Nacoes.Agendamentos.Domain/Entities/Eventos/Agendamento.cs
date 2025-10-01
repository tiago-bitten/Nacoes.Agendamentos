using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Eventos;

public sealed class Agendamento : EntityId
{
    private Agendamento()
    {
    }

    private Agendamento(
        Guid voluntarioMinisterioId,
        Guid atividadeId,
        EStatusAgendamento status,
        EOrigemAgendamento origem)
    {
        VoluntarioMinisterioId = voluntarioMinisterioId;
        AtividadeId = atividadeId;
        Status = status;
        Origem = origem;
    }

    public Guid EventoId { get; private set; }
    public Guid VoluntarioMinisterioId { get; private set; }
    public Guid AtividadeId { get; private set; }
    public EStatusAgendamento Status { get; private set; }
    public EOrigemAgendamento Origem { get; private set; }

    public Evento Evento { get; private set; } = null!;
    
    public bool PodeCancelar => Status is EStatusAgendamento.Confirmado or EStatusAgendamento.AguardandoConfirmacao;

    internal static Result<Agendamento> Criar(Guid voluntarioMinisterioId, Guid atividadeId, EOrigemAgendamento origem)
    {
        return new Agendamento(voluntarioMinisterioId, atividadeId, EStatusAgendamento.Confirmado, origem);
    }

    internal Result Cancelar()
    {
        if (!PodeCancelar)
        {
            return AgendamentoErrors.NaoEstaAgendado;
        }

        Status = EStatusAgendamento.Cancelado;

        return Result.Success();
    }
}

public enum EStatusAgendamento
{
    AguardandoConfirmacao = 0,
    Confirmado = 1,
    Cancelado = 2
}

public enum EOrigemAgendamento
{
    Manual = 0,
    Automatico = 1,
    Escala = 2
}