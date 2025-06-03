using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgenda;
public interface IAdicionarAgendaHandler
{
    Task<Result<Id<Agenda>, Error>> ExecutarAsync(AdicionarAgendaCommand command, CancellationToken cancellation = default);
}
