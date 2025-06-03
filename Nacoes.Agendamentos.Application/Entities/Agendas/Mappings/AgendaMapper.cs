using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgenda;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Mappings;

public static class AgendaMapper
{
    public static Agenda GetEntidade(this AdicionarAgendaCommand command)
        => new(command.Descricao,
               new Horario(command.Horario.DataInicial, command.Horario.DataFinal));
}
