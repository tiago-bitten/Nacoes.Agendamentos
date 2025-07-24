using FluentValidation;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarMinisterio;

public sealed class AdicionarMinisterioCommandValidator : AbstractValidator<AdicionarMinisterioCommand>
{
    public AdicionarMinisterioCommandValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
    }
}
