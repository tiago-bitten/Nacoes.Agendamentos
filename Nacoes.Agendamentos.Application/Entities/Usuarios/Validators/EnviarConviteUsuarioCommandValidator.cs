using FluentValidation;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.EnviarConvite;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Validators;

public class EnviarConviteUsuarioCommandValidator : AbstractValidator<EnviarUsuarioConviteCommand>
{
    public EnviarConviteUsuarioCommandValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}