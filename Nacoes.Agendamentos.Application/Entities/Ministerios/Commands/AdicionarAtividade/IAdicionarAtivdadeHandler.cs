using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;

public interface IAdicionarAtivdadeHandler
{
    Task<Result<Id<Atividade>, Error>> ExecutarAsync(AdicionarAtividadeCommand command, Guid ministerioId, CancellationToken cancellation = default);
}
