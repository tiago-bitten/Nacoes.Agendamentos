using Nacoes.Agendamentos.Domain.Abstracts;
using AtividadeId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Atividade>;
using VoluntarioMinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.VoluntarioMinisterio>;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas;

public sealed class Agendamento : EntityId<Agendamento>
{
    #region Constructors
    private Agendamento() { }

    internal Agendamento(VoluntarioMinisterioId voluntarioMinisterioId, AtividadeId atividadeId, EOrigemAgendamento origem)
    {
        VoluntarioMinisterioId = voluntarioMinisterioId;
        AtividadeId = atividadeId;
        Origem = origem;
        Status = EStatusAgendamento.Agendado;
    }
    #endregion

    public VoluntarioMinisterioId VoluntarioMinisterioId { get; private set; }
    public AtividadeId AtividadeId { get; private set; }
    public EStatusAgendamento Status { get; private set; }
    public EOrigemAgendamento Origem { get; private set; }

    public void Cancelar()
    {
        if (Status is not EStatusAgendamento.Agendado)
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