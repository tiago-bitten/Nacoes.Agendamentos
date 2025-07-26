using FluentValidation;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarConvite;

public sealed class EnviarConviteUsuarioCommandValidator : AbstractValidator<AdicionarUsuarioConviteCommand>
{
    public EnviarConviteUsuarioCommandValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}