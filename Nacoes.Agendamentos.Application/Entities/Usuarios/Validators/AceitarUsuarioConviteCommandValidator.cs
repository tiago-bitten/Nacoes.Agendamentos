using FluentValidation;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AceitarConvite;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Validators;

public sealed class AceitarUsuarioConviteCommandValidator : AbstractValidator<AceitarUsuarioConviteCommand>
{
    public AceitarUsuarioConviteCommandValidator()
    {
        RuleFor(x => x.Senha).NotEmpty().When(x => x.AuthType is EAuthType.Local);
        RuleFor(x => x.TokenExterno).NotEmpty().When(x => x.AuthType is not EAuthType.Local);
        RuleFor(x => x.AuthType).NotNull();
        RuleFor(x => x.UsuarioConviteId).NotNull();
        RuleFor(x => x.Celular!.Ddd).NotEmpty().When(x => x.Celular is not null);
        RuleFor(x => x.Celular!.Numero).NotEmpty().When(x => x.Celular is not null);
    }
}