using FluentValidation;

namespace Application.Authentication.Commands.LoginExterno;

internal sealed class ExternalLoginCommandValidator : AbstractValidator<ExternalLoginCommand>
{
    public ExternalLoginCommandValidator()
    {
        RuleFor(x => x.BirthDate).NotEmpty().NotNull();
        RuleFor(x => x.Cpf).NotNull().NotEmpty();
    }
}
