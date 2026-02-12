using FluentValidation;
using Domain.Voluntarios;

namespace Application.Entities.Voluntarios.Commands.Adicionar;

public sealed class AdicionarVoluntarioCommandValidator : AbstractValidator<AdicionarVoluntarioCommand>
{
    public AdicionarVoluntarioCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .NotNull()
            .WithMessage("nome");

        When(x => x.OrigemCadastro is not EOrigemCadastroVoluntario.Sistema, () =>
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("e-mail");

            RuleFor(x => x.Cpf)
                .NotNull()
                .NotEmpty()
                .WithMessage("CPF");

            RuleFor(x => x.DataNascimento)
                .NotNull()
                .WithMessage("data de nascimento");
        });

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("e-mail")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        When(x => x.Celular is not null, () =>
        {
            RuleFor(x => x.Celular!.Ddd)
                .NotEmpty()
                .NotNull()
                .WithMessage("DDD do celular é");

            RuleFor(x => x.Celular!.Numero)
                .NotEmpty()
                .NotNull()
                .WithMessage("número do celular");
        });
    }
}
