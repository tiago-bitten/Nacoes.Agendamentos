using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.ValueObjects;
using AgendaId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agenda>;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;

public interface IAgendarHandler
{
    Task<Result<Id<Agendamento>, Error>> ExcutarAsync(AgendarCommand command, AgendaId agendaId, CancellationToken cancellation = default);
}
