using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.VoluntariosMinisterios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas;
public sealed class Agendamento : EntityId<Agendamento>
{
    #region Constructors
    internal Agendamento() { }

    internal Agendamento(Id<VoluntarioMinisterio> voluntarioMinisterioId, Id<Atividade> atividadeId, EOrigemAgendamento origem)
    {
        VoluntarioMinisterioId = voluntarioMinisterioId;
        AtividadeId = atividadeId;
        Origem = origem;
        Status = EStatusAgendamento.Agendado;
    }
    #endregion

    public Id<VoluntarioMinisterio> VoluntarioMinisterioId { get; private set; }
    public Id<Atividade> AtividadeId { get; private set; }
    public EStatusAgendamento Status { get; private set; }
    public EOrigemAgendamento Origem { get; private set; }

    public void Cancelar()
    {
        if (Status != EStatusAgendamento.Agendado)
        {
            throw new InvalidOperationException("Apenas é possível cancelar agendamentos com a situação 'Agendado'.");
        }

        Status = EStatusAgendamento.Cancelado;
    }
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