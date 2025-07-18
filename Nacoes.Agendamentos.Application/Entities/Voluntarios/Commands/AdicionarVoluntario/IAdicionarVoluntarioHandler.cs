using Nacoes.Agendamentos.Domain.Common;
using VoluntarioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.Voluntario>;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.AdicionarVoluntario;

public interface IAdicionarVoluntarioHandler
{
    Task<Result<VoluntarioId>> ExecutarAsync(AdicionarVoluntarioCommand command, CancellationToken cancellation = default);
}
