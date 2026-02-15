using FluentValidation;

namespace Application.Entities.Voluntarios.Commands.Vincular;

internal sealed class LinkVolunteerMinistryCommandValidator : AbstractValidator<LinkVolunteerMinistryCommand>
{
    public LinkVolunteerMinistryCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty();
        RuleFor(x => x.MinistryId).NotEmpty();
    }
}
