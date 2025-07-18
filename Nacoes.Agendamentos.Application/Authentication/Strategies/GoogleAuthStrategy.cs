using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Infra.Settings;

namespace Nacoes.Agendamentos.Application.Authentication.Strategies;

public sealed class GoogleAuthStrategy(IUsuarioRepository usuarioRepository,
                                       IOptions<AuthenticationSettings> authSettings) : IAuthStrategy
{
    public async Task<Result<Usuario>> AutenticarAsync(LoginCommand command)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(command.TokenExterno!, GoogleSettings);

            var usuario = await usuarioRepository.RecuperarPorEmailAddressAsync(payload.Email!);
            if (usuario is null)
            {
                return GoogleAuthStrategyErrors.UsuarioNaoEncontrado;
            }

            if (usuario.AuthType is not EAuthType.Google)
            {
                return GoogleAuthStrategyErrors.AuthTypeInvalido;
            }

            return Result<Usuario>.Success(usuario);
        }
        catch
        {
            return GoogleAuthStrategyErrors.SenhaInvalida;
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
    public static readonly Error AuthTypeInvalido = 
        new("Login.Google.AuthTypeInvalido", "Autenticação inválida.");
    
    public static readonly Error SenhaInvalida = 
        new("Login.Google.SenhaInvalida", "Senha inválida.");
    
    public static readonly Error UsuarioNaoEncontrado = 
        new("Login.Google.UsuarioNaoEncontrado", "Usuário não encontrado.");
}