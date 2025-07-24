using FluentValidation;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;

internal sealed class AgendarCommandValidator : AbstractValidator<AgendarCommand>
{
    public AgendarCommandValidator()
    {
        RuleFor(x => x.VoluntarioMinisterioId).NotEmpty();
        RuleFor(x => x.AtividadeId).NotEmpty();
    }
}
