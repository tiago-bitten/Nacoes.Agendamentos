using Domain.Shared.Results;

namespace Domain.Usuarios;

public static class UserInvitationMinistryErrors
{
    public static readonly Error InvitationRequired =
        Error.Problem("user_invitation_ministry.invitation_required", "The invitation is required.");

    public static readonly Error MinistryRequired =
        Error.Problem("user_invitation_ministry.ministry_required", "The ministry is required.");
}
