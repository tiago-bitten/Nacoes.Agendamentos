using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.RecusarConvite;

internal sealed class RecusarUsuarioConviteHandler(INacoesDbContext context) 
    : ICommandHandler<RecusarUsuarioConviteCommand>
{
    public async Task<Result> Handle(RecusarUsuarioConviteCommand command, CancellationToken cancellationToken = default)
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