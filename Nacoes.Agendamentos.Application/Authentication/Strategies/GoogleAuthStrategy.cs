using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Domain.Exceptions;
using Nacoes.Agendamentos.Domain.ValueObjects;
using Nacoes.Agendamentos.Infra.Settings;

namespace Nacoes.Agendamentos.Application.Authentication.Strategies;

public sealed class GoogleAuthStrategy(IUsuarioRepository usuarioRepository,
                                       IOptions<AuthenticationSettings> authSettings) : IAuthStrategy
{
    public async Task<Usuario> AutenticarAsync(LoginCommand command)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(command.TokenExterno!, GoogleSettings);

            var usuario = await usuarioRepository.RecuperarPorEmailAddress(payload.Email!);

            usuario ??= new Usuario(payload.Name, new Email(payload.Email), EAuthType.Google);

            if (usuario.AuthType != EAuthType.Google)
            {
                Throw.AutenticacaTipoInvalido(usuario.AuthType.ToString());
            }

            return usuario;
        }
        catch (Exception)
        {
            throw Throw.SenhaInvalida();
        }
    }

    private GoogleJsonWebSignature.ValidationSettings GoogleSettings
        => new()
        {
            Audience = [authSettings.Value.Google.ClientId]
        };
}