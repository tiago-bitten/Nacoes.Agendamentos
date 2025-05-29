using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;

public interface IAdicionarUsuarioHandler
{
    Task<Id<Usuario>> ExecutarAsync(AdicionarUsuarioCommand command);
}
