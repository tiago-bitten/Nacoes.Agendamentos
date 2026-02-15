using Domain.Shared.Results;

namespace Domain.Usuarios;

public static class UserInvitationErrors
{
    public static readonly Error InvalidToken =
        Error.Problem("user_invitation.invalid_token", "Invalid token.");

    public static readonly Error InvalidStatusToAccept =
        Error.Problem(
            "user_invitation.invalid_status_to_accept",
            "Only pending invitations can be accepted.");

    public static readonly Error InvalidStatusToDecline =
        Error.Problem(
            "user_invitation.invalid_status_to_decline",
            "Only pending invitations can be declined.");

    public static readonly Error InvalidStatusToExpire =
        Error.Problem(
            "user_invitation.invalid_status_to_expire",
            "Only pending invitations can be expired.");

    public static readonly Error ExpirationDateNotReached =
        Error.Problem(
            "user_invitation.expiration_date_not_reached",
            "The invitation expiration date has not been reached.");

    public static readonly Error InvalidStatusToCancel =
        Error.Problem(
            "user_invitation.invalid_status_to_cancel",
            "Only pending invitations can be cancelled.");

    public static readonly Error ReasonRequired =
        Error.Problem("user_invitation.reason_required", "The cancellation reason is required.");

    public static readonly Error InvitationNotFound =
        Error.NotFound("user_invitation.not_found", "Invitation not found.");

    public static readonly Error PendingInvitationExists =
        Error.Problem("user_invitation.pending_exists", "A pending invitation already exists.");

    public static readonly Error InvalidStatusToPend =
        Error.Problem(
            "user_invitation.invalid_status_to_pend",
            "Only sent invitations can be set to pending.");

    public static readonly Error InvalidStatusToSend =
        Error.Problem(
            "user_invitation.invalid_status_to_send",
            "Only generated invitations can be sent.");

    public static readonly Error InvitationAlreadySent =
        Error.Problem("user_invitation.already_sent", "An invitation is already being sent. Please wait.");
}
