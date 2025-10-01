using FluentValidation;

namespace Nacoes.Agendamentos.Application.Entities.Eventos.Commands.AdicionarAgendamento;

internal sealed class AdicionarAgendamentoCommandValidator : AbstractValidator<AdicionarAgendamentoCommand>
{
    public AdicionarAgendamentoCommandValidator()
    {
        RuleFor(x => x.VoluntarioMinisterioId).NotEmpty();
        RuleFor(x => x.AtividadeId).NotEmpty();
    }
}
