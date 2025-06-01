using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarMinisterio;

public interface IAdicionarMinisterioHandler
{
    Task<Result<Id<Ministerio>, Error>> ExecutarAsync(AdicionarMinisterioCommand command);
}
