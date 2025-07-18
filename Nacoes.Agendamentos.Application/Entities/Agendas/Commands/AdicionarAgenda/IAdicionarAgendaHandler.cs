using Nacoes.Agendamentos.Domain.Common;
using AgendaId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agenda>;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgenda;
public interface IAdicionarAgendaHandler
{
    Task<Result<AgendaId>> ExecutarAsync(AdicionarAgendaCommand command, CancellationToken cancellation = default);
}
