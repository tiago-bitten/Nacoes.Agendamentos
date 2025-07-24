using FluentValidation;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;

internal sealed class AdicionarAtividadeCommandValidator : AbstractValidator<AdicionarAtividadeCommand>
{
    public AdicionarAtividadeCommandValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
    }
}
