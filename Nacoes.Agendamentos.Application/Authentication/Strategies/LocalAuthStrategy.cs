using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Authentication.PasswordVerifiers;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Specs;

namespace Nacoes.Agendamentos.Application.Authentication.Strategies;

internal class LocalAuthStrategy(INacoesDbContext context) : IAuthStrategy
{
    public async Task<Result<Usuario>> AutenticarAsync(LoginCommand command)
    {
        var usuario = await context.Usuarios
            .WhereSpec(new UsuarioComEmailAddressSpec(command.Email!))
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