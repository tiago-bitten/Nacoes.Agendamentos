using FluentValidation;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Queries.RecuperarConvitesPorToken;

public sealed class RecuperarUsuarioConvitePorTokenValidator : AbstractValidator<RecuperarUsuarioConvitePorTokenQuery>
{
    public RecuperarUsuarioConvitePorTokenValidator()
    {
        RuleFor(x => x.Token).NotNull().NotEmpty();
    }
}