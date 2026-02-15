using FluentValidation;
using Domain.Usuarios;

namespace Application.Entities.Usuarios.Commands.AceitarConvite;

internal sealed class AcceptUserInvitationCommandValidator : AbstractValidator<AcceptUserInvitationCommand>
{
    public AcceptUserInvitationCommandValidator()
    {
        RuleFor(x => x.Password).NotEmpty().When(x => x.AuthType is EAuthType.Local);
        RuleFor(x => x.ExternalToken).NotEmpty().When(x => x.AuthType is not EAuthType.Local);
        RuleFor(x => x.AuthType).NotNull();
        RuleFor(x => x.UserInvitationId).NotNull();
        RuleFor(x => x.PhoneNumber!.AreaCode).NotEmpty().When(x => x.PhoneNumber is not null);
        RuleFor(x => x.PhoneNumber!.Number).NotEmpty().When(x => x.PhoneNumber is not null);
    }
}
