using FluentValidation;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Validators;

public sealed class AdicionarAtividadeCommandValidator : AbstractValidator<AdicionarAtividadeCommand>
{
    public AdicionarAtividadeCommandValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
    }
}
