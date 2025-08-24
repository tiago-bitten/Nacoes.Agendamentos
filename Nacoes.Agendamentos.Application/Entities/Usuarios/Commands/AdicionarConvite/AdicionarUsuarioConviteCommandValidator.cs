using FluentValidation;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarConvite;

public sealed class AdicionarUsuarioConviteCommandValidator : AbstractValidator<AdicionarUsuarioConviteCommand>
{
    public AdicionarUsuarioConviteCommandValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
        RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress();
        RuleFor(x => x.MinisteriosIds).NotNull().NotEmpty();
    }
}