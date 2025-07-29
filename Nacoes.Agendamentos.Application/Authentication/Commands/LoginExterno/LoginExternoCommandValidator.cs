using FluentValidation;

namespace Nacoes.Agendamentos.Application.Authentication.Commands.LoginExterno;

public sealed class LoginExternoCommandValidator : AbstractValidator<LoginExternoCommand>
{
    public LoginExternoCommandValidator()
    {
        RuleFor(x => x.DataNascimento).NotEmpty().NotNull();
        RuleFor(x => x.Cpf).NotNull().NotEmpty();
    }
}
