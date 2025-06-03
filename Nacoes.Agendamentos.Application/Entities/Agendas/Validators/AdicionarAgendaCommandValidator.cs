using FluentValidation;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgenda;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Validators;

public sealed class AdicionarAgendaCommandValidator : AbstractValidator<AdicionarAgendaCommand>
{
    public AdicionarAgendaCommandValidator()
    {
        RuleFor(x => x.Horario.DataInicial).NotEmpty();
        RuleFor(x => x.Horario.DataFinal).NotEmpty();
        RuleFor(x => x.Descricao).NotEmpty();

        RuleFor(x => x.Horario.DataInicial).LessThan(x => x.Horario.DataFinal);
    }
}
