using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Authentication.Commands.Login;
using Application.Entities.Usuarios.Commands.Adicionar;
using Domain.Shared.Results;
using Domain.Usuarios;
using Domain.Usuarios.DomainEvents;

namespace Application.Entities.Usuarios.Commands.AceitarConvite;

internal sealed class AceitarUsuarioConviteHandler(
    INacoesDbContext context,
    ICommandHandler<AdicionarUsuarioCommand, Guid> adicionarUsuarioHandler,
    ICommandHandler<LoginCommand, LoginResponse> loginHandler)
    : ICommandHandler<AceitarUsuarioConviteCommand, AceitarUsuarioConviteResponse>
{
    public async Task<Result<AceitarUsuarioConviteResponse>> HandleAsync(
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

        var ministeriosIds = usuarioConvite.Ministerios
            .Select(x => x.MinisterioId)
            .ToList();

        var usuarioResult = await adicionarUsuarioHandler.HandleAsync(command.ToAdicionarUsuarioCommand(
            usuarioConvite.Nome,
            usuarioConvite.Email,
            ministeriosIds), cancellationToken);

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

        var loginResult = await loginHandler.HandleAsync(command.ToLoginCommand(usuarioConvite.Email), cancellationToken);
        if (loginResult.IsFailure)
        {
            return loginResult.Error;
        }

        var login = loginResult.Value;

        var response = new AceitarUsuarioConviteResponse(login.AuthToken, login.RefreshToken);

        return response;
    }
}
