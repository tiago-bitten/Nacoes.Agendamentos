using Domain.Shared.Results;

namespace Domain.Usuarios;

public static class UserMinistryErrors
{
    public static readonly Error UserRequired
        = Error.Problem("user_ministry.user_required", "The user is required.");

    public static readonly Error MinistryRequired
        = Error.Problem("user_ministry.ministry_required", "The ministry is required.");

    public static readonly Error UserAlreadyUnlinkedFromMinistry
        = Error.Problem(
            "user_ministry.user_already_unlinked",
            "The user is already unlinked from the ministry.");

    public static readonly Error UserAlreadyLinkedToMinistry
        = Error.Problem(
            "user_ministry.user_already_linked",
            "The user is already linked to the ministry.");
}
