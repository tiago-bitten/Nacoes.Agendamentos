using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgenda;

public sealed record AdicionarAgendaCommand : ICommand<Id<Agenda>>
{
    public string Descricao { get; set; } = string.Empty;
    public HorarioItem Horario { get; set; } = new();

    public record HorarioItem
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
    }
}
