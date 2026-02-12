using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Domain.Shared.Results;
using Domain.Usuarios;

namespace Application.Entities.Usuarios.Commands.RecusarConvite;

internal sealed class RecusarUsuarioConviteHandler(INacoesDbContext context)
    : ICommandHandler<RecusarUsuarioConviteCommand>
{
    public async Task<Result> HandleAsync(RecusarUsuarioConviteCommand command, CancellationToken cancellationToken = default)
    {
        var usuarioConvite = await context.Convites
            .SingleOrDefaultAsync(x => x.Id == command.UsuarioConviteId, cancellationToken);
        if (usuarioConvite is null)
        {
            return UsuarioConviteErrors.ConviteNaoEncontrado;
        }

        var recusarConviteResult = usuarioConvite.Recusar();
        if (recusarConviteResult.IsFailure)
        {
            return recusarConviteResult.Error;
        }
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
