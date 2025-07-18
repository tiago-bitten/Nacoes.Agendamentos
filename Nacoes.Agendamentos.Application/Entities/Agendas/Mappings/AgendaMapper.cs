using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgenda;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Mappings;

public static class AgendaMapper
{
    public static Result<Agenda> ToEntity(this AdicionarAgendaCommand command)
        => Agenda.Criar(command.Descricao,
                        new Horario(command.Horario.DataInicial, command.Horario.DataFinal));
}
