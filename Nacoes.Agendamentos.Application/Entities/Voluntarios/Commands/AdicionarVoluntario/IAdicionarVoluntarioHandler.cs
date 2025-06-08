using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.AdicionarVoluntario;

public interface IAdicionarVoluntarioHandler
{
    Task<Result<Id<Voluntario>, Error>> ExecutarAsync(AdicionarVoluntarioCommand command, CancellationToken cancellation = default);
}
