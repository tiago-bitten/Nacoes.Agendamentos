using FluentValidation;

namespace Application.Entities.Usuarios.Commands.RecusarConvite;

public sealed class RecusarUsuarioConviteCommandValidator : AbstractValidator<RecusarUsuarioConviteCommand>
{
    public RecusarUsuarioConviteCommandValidator()
    {
        RuleFor(x => x.UsuarioConviteId).NotNull().NotEmpty();
    }
}
