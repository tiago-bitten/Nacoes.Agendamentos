using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Adicionar;

public sealed record AdicionarAgendaCommand : ICommand<Id<Agenda>>
{
    public string Descricao { get; init; } = string.Empty;
    public HorarioItem Horario { get; init; } = new();

    public sealed record HorarioItem
    {
        public DateTime DataInicial { get; init; }
        public DateTime DataFinal { get; init; }
    }
}
