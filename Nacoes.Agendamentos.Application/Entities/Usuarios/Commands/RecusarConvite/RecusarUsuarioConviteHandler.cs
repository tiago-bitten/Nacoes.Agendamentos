using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.RecusarConvite;

internal sealed class RecusarUsuarioConviteHandler(
    INacoesDbContext context, 
    IUsuarioConviteRepository usuarioConviteRepository) 
    : ICommandHandler<RecusarUsuarioConviteCommand>
{
    public async Task<Result> Handle(RecusarUsuarioConviteCommand command, CancellationToken cancellationToken = default)
    {
        var usuarioConvite = await usuarioConviteRepository.GetByIdAsync(command.UsuarioConviteId);
        if (usuarioConvite is null)
        {
            return UsuarioConviteErrors.ConviteNaoEncontrado;
        }
        
        var recusarConviteResult = usuarioConvite.Recusar();
        if (recusarConviteResult.IsFailure)
        {
            return recusarConviteResult.Error;
        }
        
        await usuarioConviteRepository.UpdateAsync(usuarioConvite);
        await context.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}