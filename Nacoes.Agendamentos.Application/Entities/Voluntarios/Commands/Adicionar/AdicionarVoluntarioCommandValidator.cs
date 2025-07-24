using FluentValidation;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;

public sealed class AdicionarVoluntarioCommandValidator : AbstractValidator<AdicionarVoluntarioCommand>
{
    public AdicionarVoluntarioCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.");

        When(x => x.OrigemCadastro is not EOrigemCadastroVoluntario.Sistema, () =>
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("O CPF é obrigatório.");

            RuleFor(x => x.DataNascimento)
                .NotNull().WithMessage("A data de nascimento é obrigatória.");
        });

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("O e-mail informado não é válido.")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        When(x => x.Celular is not null, () =>
        {
            RuleFor(x => x.Celular!.Ddd)
                .NotEmpty().WithMessage("O DDD do celular é obrigatório.");

            RuleFor(x => x.Celular!.Numero)
                .NotEmpty().WithMessage("O número do celular é obrigatório.");
        });
    }
}