using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Common.Settings;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Authentication.Strategies;

public sealed class GoogleAuthStrategy(IUsuarioRepository usuarioRepository,
                                       IOptions<AuthenticationSettings> authSettings) 
    : IAuthStrategy
{
    public async Task<Result<Usuario>> AutenticarAsync(LoginCommand command)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(command.TokenExterno!, GoogleSettings);

            var usuario = await usuarioRepository.RecuperarPorEmailAddressAsync(payload.Email!);
            if (usuario is null)
            {
                return UsuarioErrors.NaoEncontrado;
            }

            if (usuario.AuthType is not EAuthType.Google)
            {
                return UsuarioErrors.AutenticacaoInvalida;
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