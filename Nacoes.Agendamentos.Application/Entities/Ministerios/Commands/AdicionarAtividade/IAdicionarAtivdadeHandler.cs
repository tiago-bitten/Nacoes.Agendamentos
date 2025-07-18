using Nacoes.Agendamentos.Domain.Common;
using AtividadeId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Atividade>;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;

public interface IAdicionarAtivdadeHandler
{
    Task<Result<AtividadeId>> ExecutarAsync(AdicionarAtividadeCommand command, Guid ministerioId, CancellationToken cancellation = default);
}
