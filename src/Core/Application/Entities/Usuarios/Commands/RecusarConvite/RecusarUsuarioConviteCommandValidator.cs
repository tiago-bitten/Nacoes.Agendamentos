using FluentValidation;

namespace Application.Entities.Usuarios.Commands.RecusarConvite;

internal sealed class DeclineUserInvitationCommandValidator : AbstractValidator<DeclineUserInvitationCommand>
{
    public DeclineUserInvitationCommandValidator()
    {
        RuleFor(x => x.UserInvitationId).NotNull().NotEmpty();
    }
}
