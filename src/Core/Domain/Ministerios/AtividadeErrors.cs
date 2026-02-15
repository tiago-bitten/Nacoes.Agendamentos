using Domain.Shared.Results;

namespace Domain.Ministerios;

public static class ActivityErrors
{
    public static readonly Error NameRequired =
        Error.Problem("activity.name_required", "The activity name is required.");

    public static readonly Error NameInUse =
        Error.Problem("activity.name_in_use", "An activity with this name already exists.");
}
