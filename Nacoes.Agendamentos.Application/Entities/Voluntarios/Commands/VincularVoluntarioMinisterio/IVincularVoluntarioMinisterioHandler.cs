using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.VincularVoluntarioMinisterio;

public interface IVincularVoluntarioMinisterioHandler
{
    Task<Result<Id<VoluntarioMinisterio>, Error>> ExecutarAsync(VincularVoluntarioMinisterioCommand command, CancellationToken cancellation = default);
}
