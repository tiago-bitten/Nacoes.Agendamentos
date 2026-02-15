using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Application.Shared.Contexts;
using Application.Authentication.Commands.Login;
using Application.Common.Settings;
using Application.Extensions;
using Domain.Shared.Results;
using Domain.Usuarios;
using Domain.Usuarios.Specs;

namespace Application.Authentication.Strategies;

internal sealed class GoogleAuthStrategy(INacoesDbContext context, IOptions<AuthenticationSettings> authSettings)
    : IAuthStrategy
{
    public async Task<Result<User>> AuthenticateAsync(LoginCommand command, CancellationToken ct)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(command.ExternalToken!, GoogleSettings);

            var user = await context.Users
                .ApplySpec(new UserWithEmailAddressSpec(payload.Email!))
                .Where(x => x.AuthType == EAuthType.Google)
                .AsNoTracking()
                .SingleOrDefaultAsync(ct);
            if (user is null)
            {
                return UserErrors.NotFound;
            }

            return user;
        }
        catch (InvalidJwtException ex)
        {
            return GoogleAuthStrategyErrors.InvalidJwt(ex.Message);
        }

        catch (Exception)
        {
            return UserErrors.InvalidPassword;
        }
    }

    private GoogleJsonWebSignature.ValidationSettings GoogleSettings
        => new()
        {
            Audience = [authSettings.Value.Google.ClientId]
        };
}

public static class GoogleAuthStrategyErrors
{
    public static Error InvalidJwt(string googleMessage)
        => Error.Problem("Login.Google.InvalidJwt", $"Error: {googleMessage}");
}
