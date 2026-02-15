using FluentValidation;
using Domain.Usuarios;

namespace Application.Entities.Usuarios.Commands.Adicionar;

internal sealed class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(User.NameMaxLength);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().When(x => x.AuthType is EAuthType.Local);
        RuleFor(x => x.AuthType).NotNull();
        RuleFor(x => x.PhoneNumber!.AreaCode).NotEmpty().When(x => x.PhoneNumber is not null);
        RuleFor(x => x.PhoneNumber!.Number).NotEmpty().When(x => x.PhoneNumber is not null);
    }
}
