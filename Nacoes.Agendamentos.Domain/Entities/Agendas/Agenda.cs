using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.Exceptions;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas;
public sealed class Agenda : EntityId<Agenda>, IAggregateRoot
{
    private List<Agendamento> _agendamentos = [];

    #region Constructors
    internal Agenda() { }

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
    public void AtualizarHorario(Horario horario, bool forcar = false)
    {
        if (horario < Horario && !forcar)
        {
            throw new ArgumentException("O horário atualizado deve ser maior ou igual ao horário atual.", nameof(horario));
        }

        Horario = horario;
    }
    #endregion

    #region Agendar
    public void Agendar(Id<VoluntarioMinisterio> voluntarioMinisterioId, Id<Atividade> atividadeId, EOrigemAgendamento origem)
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
