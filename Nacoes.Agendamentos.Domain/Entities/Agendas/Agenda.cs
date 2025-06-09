using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Exceptions;
using Nacoes.Agendamentos.Domain.ValueObjects;
using VoluntarioMinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.VoluntarioMinisterio>;
using AtividadeId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Atividade>;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas;

public sealed class Agenda : EntityId<Agenda>, IAggregateRoot
{
    private List<Agendamento> _agendamentos = [];

    #region Constructors
    private Agenda() { }

    public Agenda(string descricao, Horario horario)
    {
        if (string.IsNullOrEmpty(descricao))
        {
            throw new ArgumentNullException(nameof(descricao), "A descrição da agenda é obrigatória.");
        }

        Descricao = descricao;
        Horario = horario;
    }
    #endregion

    public string Descricao { get; private set; }
    public Horario Horario { get; private set; }

    public IReadOnlyCollection<Agendamento> Agendamentos => _agendamentos.AsReadOnly();

    #region AtualizarHorario
    public void AtualizarHorario(Horario horario)
    {
        Horario = horario;
    }
    #endregion

    #region Agendar
    public void Agendar(VoluntarioMinisterioId voluntarioMinisterioId, AtividadeId atividadeId, EOrigemAgendamento origem)
    {
        var voluntarioJaAgendado = _agendamentos.Any(a => a.VoluntarioMinisterioId == voluntarioMinisterioId);
        if (voluntarioJaAgendado)
        {
            throw ExceptionFactory.VoluntarioJaAgendado();
        }

        var agendamento = new Agendamento(voluntarioMinisterioId, atividadeId, origem);
        _agendamentos.Add(agendamento);
    }
    #endregion
}
