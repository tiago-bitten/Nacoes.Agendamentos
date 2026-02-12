using FluentValidation;

namespace Application.Entities.Ministerios.Commands.AdicionarAtividade;

internal sealed class AdicionarAtividadeCommandValidator : AbstractValidator<AdicionarAtividadeCommand>
{
    public AdicionarAtividadeCommandValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
    }
}
