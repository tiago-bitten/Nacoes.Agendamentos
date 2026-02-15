using Domain.Shared.Results;

namespace Domain.Voluntarios.Errors;

public static class VolunteerErrors
{
    public static readonly Error NotFound =
        Error.NotFound("volunteer.not_found", "Volunteer not found.");

    public static readonly Error NameRequired =
        Error.Problem("volunteer.name_required", "The volunteer name is required.");

    public static readonly Error PersonalDataRequired =
        Error.Problem(
            "volunteer.personal_data_required",
            "Personal data is required for non-system registration.");

    public static readonly Error InvalidAuthentication =
        Error.Problem("volunteer.invalid_authentication", "CPF and birth date are required.");

    public static readonly Error LoginNotFound =
        Error.NotFound("volunteer.login_not_found", "Could not find your registration.");

    public static readonly Error MustCreateAccount =
        Error.Problem("volunteer.must_create_account", "You need to create an account to proceed.");
}
