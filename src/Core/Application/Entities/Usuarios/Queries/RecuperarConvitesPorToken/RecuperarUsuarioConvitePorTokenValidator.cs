using FluentValidation;

namespace Application.Entities.Usuarios.Queries.RecuperarConvitesPorToken;

internal sealed class GetUserInvitationByTokenQueryValidator : AbstractValidator<GetUserInvitationByTokenQuery>
{
    public GetUserInvitationByTokenQueryValidator()
    {
        RuleFor(x => x.Token).NotNull().NotEmpty();
    }
}
