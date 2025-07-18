using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.VincularVoluntarioMinisterio;

public interface IVincularVoluntarioMinisterioHandler
{
    Task<Result> ExecutarAsync(VincularVoluntarioMinisterioCommand command, CancellationToken cancellation = default);
}
