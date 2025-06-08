using FluentValidation;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.VincularVoluntarioMinisterio;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Validators;

public sealed class VincularVoluntarioMinisterioCommandValidator : AbstractValidator<VincularVoluntarioMinisterioCommand>
{
    public VincularVoluntarioMinisterioCommandValidator()
    {
        RuleFor(x => x.VoluntarioId).NotEmpty();
        RuleFor(x => x.MinisterioId).NotEmpty();
    }
}
