using FluentValidation;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;

public sealed class AdicionarUsuarioCommandValidator : AbstractValidator<AdicionarUsuarioCommand>
{ 
  public AdicionarUsuarioCommandValidator()
  {
    RuleFor(x => x.Nome).NotEmpty();
    RuleFor(x => x.Email).NotEmpty().EmailAddress();
    RuleFor(x => x.Senha).NotEmpty().When(x => x.AuthType is EAuthType.Local);
    RuleFor(x => x.AuthType).NotNull();
    RuleFor(x => x.Celular!.Ddd).NotEmpty().When(x => x.Celular is not null);
    RuleFor(x => x.Celular!.Numero).NotEmpty().When(x => x.Celular is not null);
  }  
}