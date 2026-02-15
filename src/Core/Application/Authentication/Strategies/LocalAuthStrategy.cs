using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Authentication.Commands.Login;
using Application.Authentication.PasswordVerifiers;
using Application.Extensions;
using Domain.Shared.Results;
using Domain.Usuarios;
using Domain.Usuarios.Specs;

namespace Application.Authentication.Strategies;

internal sealed class LocalAuthStrategy(INacoesDbContext context) : IAuthStrategy
{
    public async Task<Result<User>> AuthenticateAsync(LoginCommand command, CancellationToken ct)
    {
        var user = await context.Users
            .ApplySpec(new UserWithEmailAddressSpec(command.Email!))
            .Where(x => x.AuthType == EAuthType.Local)
            .AsNoTracking()
            .SingleOrDefaultAsync(ct);
        if (user is null)
        {
            return UserErrors.NotFound;
        }

        if (user.AuthType is not EAuthType.Local)
        {
            return UserErrors.InvalidAuthentication;
        }

        var passwordValid = PasswordHelper.Verify(command.Password!, user.Password!);
        if (!passwordValid)
        {
            return UserErrors.InvalidPassword;
        }

        return user;
    }
}
