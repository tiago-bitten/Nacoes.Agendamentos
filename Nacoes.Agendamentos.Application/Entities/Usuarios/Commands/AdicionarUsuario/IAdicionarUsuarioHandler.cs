using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarUsuario;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;

public interface IAdicionarUsuarioHandler
{
    Task<Result<Id<Usuario>, Error>> ExecutarAsync(AdicionarUsuarioCommand command, CancellationToken cancellation = default);
}
