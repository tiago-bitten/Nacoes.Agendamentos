using FluentValidation;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.AdicionarVoluntario;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Validators;

public sealed class AdicionarVoluntarioCommandValidator : AbstractValidator<AdicionarVoluntarioCommand>
{
    public AdicionarVoluntarioCommandValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
        RuleFor(x => x.Email).EmailAddress().When(x => x.Email is not null);
    }
}