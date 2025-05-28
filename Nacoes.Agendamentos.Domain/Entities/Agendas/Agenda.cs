using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas;
public sealed class Agenda : EntityId<Agenda>, IAggregateRoot
{
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
}
