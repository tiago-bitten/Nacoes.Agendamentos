using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Common.Settings;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Authentication.Strategies;

internal sealed class GoogleAuthStrategy(IUsuarioRepository usuarioRepository,
                                         IOptions<AuthenticationSettings> authSettings) 
    : IAuthStrategy
{
    public async Task<Result<Usuario>> AutenticarAsync(LoginCommand command)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(command.TokenExterno!, GoogleSettings);

            var usuario = await usuarioRepository.RecuperarPorEmailAddress(payload.Email!)
                                                 .Where(x => x.AuthType == EAuthType.Google)
                                                 .AsNoTracking()
                                                 .SingleOrDefaultAsync();
            if (usuario is null)
            {
                return UsuarioErrors.NaoEncontrado;
            }

            return Result<Usuario>.Success(usuario);
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