using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Authentication.PasswordVerifiers;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Authentication.Strategies;

public class LocalAuthStrategy(IUsuarioRepository usuarioRepository) : IAuthStrategy
{
    public async Task<Result<Usuario>> AutenticarAsync(LoginCommand command)
    {
        var usuario = await usuarioRepository.RecuperarPorEmailAddressAsync(command.Email!);
        if (usuario is null)
        {
            return UsuarioErrors.NaoEncontrado;
        }
            
        if (usuario.AuthType is not EAuthType.Local)
        {
            return UsuarioErrors.AutenticacaoInvalida;
        }

        var senhaValida = PasswordHelper.Verify(command.Senha!, usuario.Senha!);
        if (!senhaValida)
        {
            return UsuarioErrors.SenhaInvalida;
        }

        return Result<Usuario>.Success(usuario);
    }
}