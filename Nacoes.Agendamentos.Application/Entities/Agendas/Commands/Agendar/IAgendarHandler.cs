using Nacoes.Agendamentos.Domain.Common;
using AgendamentoId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agendamento>;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;

public interface IAgendarHandler
{
    Task<Result<AgendamentoId>> ExecutarAsync(AgendarCommand command, CancellationToken cancellation = default);
}
