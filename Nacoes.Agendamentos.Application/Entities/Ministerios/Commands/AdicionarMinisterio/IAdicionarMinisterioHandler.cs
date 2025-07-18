using Nacoes.Agendamentos.Domain.Common;
using MinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Ministerio>;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarMinisterio;

public interface IAdicionarMinisterioHandler
{
    Task<Result<MinisterioId>> ExecutarAsync(AdicionarMinisterioCommand command);
}
