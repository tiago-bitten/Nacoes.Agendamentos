using Microsoft.AspNetCore.Identity.Data;
using Application.Authentication.Commands.Login;
using Domain.Shared.Results;
using Domain.Usuarios;

namespace Application.Authentication.Strategies;
public interface IAuthStrategy
{
    Task<Result<Usuario>> AutenticarAsync(LoginCommand command);
}
