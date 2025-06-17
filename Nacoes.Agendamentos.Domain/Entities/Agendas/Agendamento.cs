using Nacoes.Agendamentos.Domain.Abstracts;
using AgendaId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agenda>;
using AtividadeId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Atividade>;
using VoluntarioMinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.VoluntarioMinisterio>;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas;

public sealed class Agendamento : EntityId<Agendamento>
{
    #region Construtores
    private Agendamento() { }

    public Agendamento(AgendaId agendaId, 
                       VoluntarioMinisterioId voluntarioMinisterioId,
                       AtividadeId atividadeId,
                       EOrigemAgendamento origem)
    {
        AgendaId = agendaId;
        VoluntarioMinisterioId = voluntarioMinisterioId;
        AtividadeId = atividadeId;
        Origem = origem;
        Status = EStatusAgendamento.Agendado;
    }
    #endregion

    public AgendaId AgendaId { get; private set; }
    public VoluntarioMinisterioId VoluntarioMinisterioId { get; private set; }
    public AtividadeId AtividadeId { get; private set; }
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
    Agendado = 1,
    Cancelado = 2
}

public enum EOrigemAgendamento
{
    Manual = 1,
    Automatico = 2,
    Escala = 3
}