using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Domain.Shared.Results;
using Domain.Usuarios;

namespace Application.Entities.Usuarios.Commands.RecusarConvite;

internal sealed class DeclineUserInvitationHandler(INacoesDbContext context)
    : ICommandHandler<DeclineUserInvitationCommand>
{
    public async Task<Result> HandleAsync(
        DeclineUserInvitationCommand command,
        CancellationToken ct)
    {
        var invitation = await context.Invitations
            .SingleOrDefaultAsync(x => x.Id == command.UserInvitationId, ct);
        if (invitation is null)
        {
            return UserInvitationErrors.InvitationNotFound;
        }

        var declineResult = invitation.Decline();
        if (declineResult.IsFailure)
        {
            return declineResult.Error;
        }
        await context.SaveChangesAsync(ct);

        return Result.Success();
    }
}
