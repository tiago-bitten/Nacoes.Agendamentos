using FluentValidation;

namespace Application.Entities.Voluntarios.Commands.Vincular;

public sealed class VincularVoluntarioMinisterioCommandValidator : AbstractValidator<VincularVoluntarioMinisterioCommand>
{
    public VincularVoluntarioMinisterioCommandValidator()
    {
        RuleFor(x => x.VoluntarioId).NotEmpty();
        RuleFor(x => x.MinisterioId).NotEmpty();
    }
}
