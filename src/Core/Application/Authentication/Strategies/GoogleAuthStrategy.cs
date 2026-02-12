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
    public async Task<Result<Usuario>> AutenticarAsync(LoginCommand command)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(command.TokenExterno!, GoogleSettings);

            var usuario = await context.Usuarios
                .ApplySpec(new UsuarioComEmailAddressSpec(payload.Email!))
                .Where(x => x.AuthType == EAuthType.Google)
                .AsNoTracking()
                .SingleOrDefaultAsync();
            if (usuario is null)
            {
                return UsuarioErrors.NaoEncontrado;
            }

            return usuario;
        }
        catch (InvalidJwtException ex)
        {
            return GoogleAuthStrategyErrors.JwtInvalido(ex.Message);
        }

        catch (Exception)
        {
            return UsuarioErrors.SenhaInvalida;
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
    public static Error JwtInvalido(string googleMessage)
        => Error.Problem("Login.Google.JwtInvalido", $"Erro: {googleMessage}");
}
