using FluentValidation;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Validators;

public sealed class AdicionarVoluntarioCommandValidator : AbstractValidator<AdicionarVoluntarioCommand>
{
    public AdicionarVoluntarioCommandValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress().When(x => x.OrigemCadastro is not EOrigemCadastroVoluntario.Sistema);
        RuleFor(x => x.Cpf).NotEmpty().When(x => x.OrigemCadastro is not EOrigemCadastroVoluntario.Sistema);
        RuleFor(x => x.DataNascimento).NotNull().When(x => x.OrigemCadastro is not EOrigemCadastroVoluntario.Sistema);
        RuleFor(x => x.Celular!.Ddd).NotEmpty().When(x => x.Celular is not null);
        RuleFor(x => x.Celular!.Numero).NotEmpty().When(x => x.Celular is not null);
    }
}