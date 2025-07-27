using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas;

public sealed class Agendamento : EntityId
{
    #region Construtores
    private Agendamento() { }

    public Agendamento(Guid agendaId, 
                       Guid voluntarioMinisterioId,
                       Guid atividadeId,
                       EOrigemAgendamento origem)
    {
        AgendaId = agendaId;
        VoluntarioMinisterioId = voluntarioMinisterioId;
        AtividadeId = atividadeId;
        Origem = origem;
        Status = EStatusAgendamento.Agendado;
    }
    #endregion

    public Guid AgendaId { get; private set; }
    public Guid VoluntarioMinisterioId { get; private set; }
    public Guid AtividadeId { get; private set; }
    public EStatusAgendamento Status { get; private set; }
    public EOrigemAgendamento Origem { get; private set; }

    #region Cancelar
    public void Cancelar()
    {
        if (Status is not EStatusAgendamento.Agendado)
        {
            throw new InvalidOperationException("Apenas é possível cancelar agendamentos com a situação 'Agendado'.");
        }

        Status = EStatusAgendamento.Cancelado;
    }
    #endregion
}

public enum EStatusAgendamento
{
    Agendado = 0,
    Cancelado = 1
}

public enum EOrigemAgendamento
{
    Manual = 1,
    Automatico = 2,
    Escala = 3
}