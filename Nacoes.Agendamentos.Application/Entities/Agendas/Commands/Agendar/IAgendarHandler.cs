using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;

public interface IAgendarHandler
{
    Task<Result<Id<Agendamento>, Error>> ExecutarAsync(AgendarCommand command, CancellationToken cancellation = default);
}
