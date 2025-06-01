using FluentValidation;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarMinisterio;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Validators;

public sealed class AdicionarMinisterioCommandValidator : AbstractValidator<AdicionarMinisterioCommand>
{
    public AdicionarMinisterioCommandValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
    }
}
