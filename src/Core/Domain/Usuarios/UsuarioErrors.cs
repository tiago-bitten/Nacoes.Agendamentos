using Domain.Shared.Results;

namespace Domain.Usuarios;

public static class UserErrors
{
    public static readonly Error PasswordTooShort =
        Error.Problem("user.password_too_short", "Password must be at least 4 characters.");

    public static readonly Error NameRequired =
        Error.Problem("user.name_required", "The user name is required.");

    public static readonly Error PasswordNotRequired =
        Error.Problem(
            "user.password_not_required",
            "Non-local authentication does not require a password.");

    public static readonly Error EmailInUse =
        Error.Conflict("user.email_in_use", "The provided email is already in use.");

    public static readonly Error NotFound =
        Error.NotFound("user.not_found", "User not found.");

    public static readonly Error InvalidAuthentication =
        Error.Problem("user.invalid_authentication", "Invalid authentication.");

    public static readonly Error InvalidPassword =
        Error.Problem("user.invalid_password", "Invalid password.");

    public static readonly Error MinistriesRequired =
        Error.Problem(
            "user.ministries_required",
            "The user must be linked to at least one ministry.");

    public static readonly Error MinistryNotLinkedToUser =
        Error.Problem(
            "user.ministry_not_linked_to_user",
            "The specified ministry is not linked to the user.");

    public static readonly Error MinistryAlreadyLinkedToUser =
        Error.Problem(
            "user.ministry_already_linked_to_user",
            "The specified ministry is already linked to the user.");
}
