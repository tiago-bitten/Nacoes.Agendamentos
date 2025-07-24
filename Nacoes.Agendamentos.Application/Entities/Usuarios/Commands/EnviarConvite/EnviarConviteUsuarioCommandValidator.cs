using FluentValidation;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.EnviarConvite;

public sealed class EnviarConviteUsuarioCommandValidator : AbstractValidator<EnviarUsuarioConviteCommand>
{
    public EnviarConviteUsuarioCommandValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}