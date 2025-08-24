using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.DomainEvents;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AceitarConvite;

internal sealed class AceitarUsuarioConviteHandler(
    INacoesDbContext context, 
    ICommandHandler<AdicionarUsuarioCommand, Guid> adicionarUsuarioHandler, 
    ICommandHandler<LoginCommand, LoginResponse> loginHandler)
    : ICommandHandler<AceitarUsuarioConviteCommand, AceitarUsuarioConviteResponse>
{
    public async Task<Result<AceitarUsuarioConviteResponse>> Handle(
        AceitarUsuarioConviteCommand command,
        CancellationToken cancellationToken = default)
    {
        var usuarioConvite = await context.Convites
            .Include(x => x.Ministerios)
            .SingleOrDefaultAsync(x => x.Id == command.UsuarioConviteId, cancellationToken);
        if (usuarioConvite is null)
        {
            return UsuarioConviteErrors.ConviteNaoEncontrado;
        }

        var usuarioResult = await adicionarUsuarioHandler.Handle(command.ToAdicionarUsuarioCommand(
            usuarioConvite.Nome,
            usuarioConvite.Email,
            usuarioConvite.Ministerios
                .Select(x => x.MinisterioId)
                .ToList()), cancellationToken);
        
        if (usuarioResult.IsFailure)
        {
            return usuarioResult.Error;
        }

        var usuario = usuarioResult.Value;
        
        var aceitarUsuarioConviteResult = usuarioConvite.Aceitar(usuario);
        if (aceitarUsuarioConviteResult.IsFailure)
        {
            return aceitarUsuarioConviteResult.Error;
        }
        
        usuarioConvite.Raise(new UsuarioConviteAceitoDomainEvent(usuarioConvite.Id));
        await context.SaveChangesAsync(cancellationToken);
        
        var loginResult = await loginHandler.Handle(command.ToLoginCommand(usuarioConvite.Email), cancellationToken);
        if (loginResult.IsFailure)
        {
            return loginResult.Error;
        }
        
        var login = loginResult.Value;

        var response = new AceitarUsuarioConviteResponse(login.AuthToken, login.RefreshToken);

        return response;
    }
}