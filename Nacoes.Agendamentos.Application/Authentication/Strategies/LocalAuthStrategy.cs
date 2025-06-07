using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Authentication.PasswordVerifiers;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Domain.Exceptions;
using Nacoes.Agendamentos.Infra.Extensions;

namespace Nacoes.Agendamentos.Application.Authentication.Strategies;

public class LocalAuthStrategy(IUsuarioRepository usuarioRepository) : IAuthStrategy
{
    public async Task<Usuario> AutenticarAsync(LoginCommand command)
    {
        var usuario = await usuarioRepository.RecuperarPorEmailAddress(command.Email)
                                             .OrElse(ExceptionFactory.UsuarioNaoEncontrado);

        if (usuario.AuthType != EAuthType.Local)
        {
            throw ExceptionFactory.AutenticacaTipoInvalido(usuario.AuthType.ToString());
        }

        var senhaValida = PasswordVerifier.Execute(command.Senha!, usuario.Senha!);

        if (!senhaValida)
        {
            throw ExceptionFactory.SenhaInvalida();
        }

        return usuario;
    }
}
