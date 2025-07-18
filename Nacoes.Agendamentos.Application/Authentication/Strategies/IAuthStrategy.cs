using Microsoft.AspNetCore.Identity.Data;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Authentication.Strategies;
public interface IAuthStrategy
{
    Task<Result<Usuario>> AutenticarAsync(LoginCommand command);
}
