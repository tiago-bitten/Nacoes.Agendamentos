using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Authentication.Context;
using Application.Extensions;
using Domain.Shared.Results;
using Domain.Shared.Factories;
using Domain.Usuarios;
using Domain.Usuarios.DomainEvents;
using Domain.Usuarios.Specs;

namespace Application.Entities.Usuarios.Commands.AdicionarConvite;

internal sealed class AddUserInvitationHandler(
    INacoesDbContext context,
    IEnvironmentContext environmentContext,
    ILinkFactory linkFactory)
    : ICommandHandler<AddUserInvitationCommand, UserInvitationResponse>
{
    public async Task<Result<UserInvitationResponse>> HandleAsync(
        AddUserInvitationCommand command,
        CancellationToken ct)
    {
        var pendingInvitationExists = await context.Invitations
            .ApplySpec(new PendingInvitationsSpec())
            .Where(x => x.Email.Address == command.EmailAddress)
            .AnyAsync(ct);

        if (pendingInvitationExists)
        {
            return UserInvitationErrors.PendingInvitationExists;
        }

        var invitationResult = UserInvitation.Create(
            command.Name,
            command.EmailAddress,
            environmentContext.UserId,
            command.MinistryIds);
        if (invitationResult.IsFailure)
        {
            return invitationResult.Error;
        }

        var invitation = invitationResult.Value;

        var link = linkFactory.Create(invitation.Path);
        var response = new UserInvitationResponse(link);

        await context.Invitations.AddAsync(invitation, ct);
        await context.SaveChangesAsync(ct);

        return response;
    }
}
