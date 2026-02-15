using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Authentication.Commands.Login;
using Application.Entities.Usuarios.Commands.Adicionar;
using Domain.Shared.Results;
using Domain.Usuarios;
using Domain.Usuarios.DomainEvents;

namespace Application.Entities.Usuarios.Commands.AceitarConvite;

internal sealed class AcceptUserInvitationHandler(
    INacoesDbContext context,
    ICommandHandler<AddUserCommand, Guid> addUserHandler,
    ICommandHandler<LoginCommand, LoginResponse> loginHandler)
    : ICommandHandler<AcceptUserInvitationCommand, AcceptUserInvitationResponse>
{
    public async Task<Result<AcceptUserInvitationResponse>> HandleAsync(
        AcceptUserInvitationCommand command,
        CancellationToken ct)
    {
        var invitation = await context.Invitations
            .Include(x => x.Ministries)
            .SingleOrDefaultAsync(x => x.Id == command.UserInvitationId, ct);
        if (invitation is null)
        {
            return UserInvitationErrors.InvitationNotFound;
        }

        var ministryIds = invitation.Ministries
            .Select(x => x.MinistryId)
            .ToList();

        var userResult = await addUserHandler.HandleAsync(command.ToAddUserCommand(
            invitation.Name,
            invitation.Email,
            ministryIds), ct);

        if (userResult.IsFailure)
        {
            return userResult.Error;
        }

        var userId = userResult.Value;

        var acceptResult = invitation.Accept(userId);
        if (acceptResult.IsFailure)
        {
            return acceptResult.Error;
        }

        invitation.Raise(new UserInvitationAcceptedDomainEvent(invitation.Id));
        await context.SaveChangesAsync(ct);

        var loginResult = await loginHandler.HandleAsync(command.ToLoginCommand(invitation.Email), ct);
        if (loginResult.IsFailure)
        {
            return loginResult.Error;
        }

        var login = loginResult.Value;

        var response = new AcceptUserInvitationResponse(login.AuthToken, login.RefreshToken);

        return response;
    }
}
