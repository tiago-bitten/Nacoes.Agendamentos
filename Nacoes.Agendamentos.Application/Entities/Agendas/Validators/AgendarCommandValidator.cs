using FluentValidation;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Validators;

public sealed class AgendarCommandValidator : AbstractValidator<AgendarCommand>
{
    public AgendarCommandValidator()
    {
        RuleFor(x => x.VoluntarioMinisterioId).NotEmpty();
        RuleFor(x => x.AtividadeId).NotEmpty();
    }
}
