using FluentValidation;
using Domain.Usuarios;

namespace Application.Authentication.Commands.Login;

internal sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().When(x => x.AuthType is EAuthType.Local).EmailAddress();
        RuleFor(x => x.AuthType).NotNull();
        RuleFor(x => x.Password).NotEmpty().When(x => x.AuthType is EAuthType.Local);
        RuleFor(x => x.ExternalToken).NotEmpty().When(x => x.AuthType is not EAuthType.Local);
    }
}
