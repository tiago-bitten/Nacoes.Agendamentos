using FluentValidation;
using Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Validators;

public sealed class AdicionarUsuarioCommandValidator : AbstractValidator<AdicionarUsuarioCommand>
{
    public AdicionarUsuarioCommandValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
        RuleFor(x => x.Email).NotNull().EmailAddress();
        RuleFor(x => x.Celular).NotNull();
        RuleFor(x => x.AuthType).NotEmpty();
        RuleFor(x => x.Senha).NotEmpty().When(x => x.AuthType == EAuthType.Local);
        RuleFor(x => x.Ministerios).NotEmpty();
    }
}
