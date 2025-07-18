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
            return LocalAuthStrategyErrors.UsuarioNaoEncontrado;
        }
            
        if (usuario.AuthType != EAuthType.Local)
        {
            return LocalAuthStrategyErrors.AutenticacaTipoInvalido;
        }

        var senhaValida = PasswordVerifier.Execute(command.Senha!, usuario.Senha!);
        if (!senhaValida)
        {
            return LocalAuthStrategyErrors.SenhaInvalida;
        }

        return Result<Usuario>.Success(usuario);
    }
}

public static class LocalAuthStrategyErrors
{
    public static readonly Error SenhaInvalida = 
        new("Login.Local.SenhaInvalida", "A senha informada é inválida.");
    
    public static readonly Error AutenticacaTipoInvalido = 
        new("Login.Local.AutenticacaTipoInvalido", "O tipo de autenticação informado é inválido.");
    
    public static readonly Error UsuarioNaoEncontrado = 
        new("Login.Local.UsuarioNaoEncontrado", "O usuário informado não foi encontrado.");
}
