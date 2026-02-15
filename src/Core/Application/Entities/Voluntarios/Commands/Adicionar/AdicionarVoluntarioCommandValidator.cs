using FluentValidation;
using Domain.Voluntarios;

namespace Application.Entities.Voluntarios.Commands.Adicionar;

internal sealed class AddVolunteerCommandValidator : AbstractValidator<AddVolunteerCommand>
{
    public AddVolunteerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(Volunteer.NameMaxLength)
            .WithMessage("name");

        When(x => x.RegistrationOrigin is not EVolunteerRegistrationOrigin.System, () =>
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("email");

            RuleFor(x => x.Cpf)
                .NotNull()
                .NotEmpty()
                .WithMessage("CPF");

            RuleFor(x => x.BirthDate)
                .NotNull()
                .WithMessage("birth date");
        });

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("email")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        When(x => x.PhoneNumber is not null, () =>
        {
            RuleFor(x => x.PhoneNumber!.AreaCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("phone number area code");

            RuleFor(x => x.PhoneNumber!.Number)
                .NotEmpty()
                .NotNull()
                .WithMessage("phone number");
        });
    }
}
