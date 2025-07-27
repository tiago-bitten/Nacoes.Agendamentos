using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas;

public sealed class Agenda : EntityId, IAggregateRoot
{
    private readonly List<Agendamento> _agendamentos = [];

    private Agenda() { }

    private Agenda(string descricao, Horario horario)
    {
        Descricao = descricao;
        Horario = horario;
    }

    public string Descricao { get; private set; } = null!;
    public Horario Horario { get; private set; } = null!;

    public IReadOnlyCollection<Agendamento> Agendamentos => _agendamentos.AsReadOnly();
    
    #region Criar

    public static Result<Agenda> Criar(string descricao, Horario horario)
    {
        if (string.IsNullOrWhiteSpace(descricao))
        {
            return AgendaErrors.DescricaoObrigatoria;
        }

        var agenda = new Agenda(descricao, horario);
        return Result<Agenda>.Success(agenda);
    }

    #endregion
}
