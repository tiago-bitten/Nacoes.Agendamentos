using Domain.Shared.Results;

namespace Domain.Voluntarios.Errors;

public static class VolunteerMinistryErrors
{
    public static readonly Error VolunteerAlreadyLinked =
        Error.Problem(
            "volunteer_ministry.already_linked",
            "Volunteer is already linked to this ministry.");

    public static readonly Error VolunteerAlreadyUnlinked =
        Error.Problem(
            "volunteer_ministry.already_unlinked",
            "Volunteer is already unlinked from this ministry.");

    public static readonly Error VolunteerAlreadySuspended =
        Error.Problem("volunteer_ministry.already_suspended", "Volunteer is already suspended in this ministry.");

    public static readonly Error VolunteerNotLinkedToMinistry =
        Error.Problem(
            "volunteer_ministry.not_linked",
            "Volunteer is not linked to this ministry.");
}
