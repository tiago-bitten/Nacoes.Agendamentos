using Domain.Shared.Results;

namespace Domain.Ministerios;

public static class MinistryErrors
{
    public static readonly Error NotFound =
        Error.NotFound("ministry.not_found", "Ministry not found.");

    public static readonly Error NameRequired =
        Error.Problem("ministry.name_required", "The ministry name is required.");

    public static readonly Error ColorRequired =
        Error.Problem("ministry.color_required", "The ministry color is required.");
}
