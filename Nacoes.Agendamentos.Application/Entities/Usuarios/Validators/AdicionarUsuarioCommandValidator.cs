using FluentValidation;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarUsuario;
using Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Validators;

public sealed class AdicionarUsuarioCommandValidator : AbstractValidator<AdicionarUsuarioCommand>
{
    public AdicionarUsuarioCommandValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().When(x => x.AuthType == EAuthType.Local);
        RuleFor(x => x.Email).NotNull().EmailAddress().When(x => x.AuthType == EAuthType.Local);
        RuleFor(x => x.Celular).NotNull().When(x => x.AuthType == EAuthType.Local);
        RuleFor(x => x.AuthType).NotEmpty();
        RuleFor(x => x.Senha).NotEmpty().When(x => x.AuthType == EAuthType.Local);
        RuleFor(x => x.TokenExterno).NotEmpty().When(x => x.AuthType != EAuthType.Local);
        RuleFor(x => x.Ministerios).NotEmpty();
    }
}
