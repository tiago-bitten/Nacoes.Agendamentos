using FluentValidation;

namespace Application.Entities.Voluntarios.Commands.Desvincular;

internal sealed class UnlinkVolunteerMinistryCommandValidator : AbstractValidator<UnlinkVolunteerMinistryCommand>
{
    public UnlinkVolunteerMinistryCommandValidator()
    {
        RuleFor(x => x.VolunteerMinistryId).NotEmpty().NotNull();
    }
}
