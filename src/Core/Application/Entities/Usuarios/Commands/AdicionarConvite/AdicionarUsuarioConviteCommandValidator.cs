using FluentValidation;
using Domain.Usuarios;

namespace Application.Entities.Usuarios.Commands.AdicionarConvite;

internal sealed class AddUserInvitationCommandValidator : AbstractValidator<AddUserInvitationCommand>
{
    public AddUserInvitationCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(UserInvitation.NameMaxLength);
        RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress();
        RuleFor(x => x.MinistryIds).NotNull().NotEmpty();
    }
}
