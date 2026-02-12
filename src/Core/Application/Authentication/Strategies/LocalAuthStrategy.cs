using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Authentication.Commands.Login;
using Application.Authentication.PasswordVerifiers;
using Application.Extensions;
using Domain.Shared.Results;
using Domain.Usuarios;
using Domain.Usuarios.Specs;

namespace Application.Authentication.Strategies;

internal class LocalAuthStrategy(INacoesDbContext context) : IAuthStrategy
{
    public async Task<Result<Usuario>> AutenticarAsync(LoginCommand command)
    {
        var usuario = await context.Usuarios
            .ApplySpec(new UsuarioComEmailAddressSpec(command.Email!))
            .Where(x => x.AuthType == EAuthType.Local)
            .AsNoTracking()
            .SingleOrDefaultAsync();
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

        return usuario;
    }
}
