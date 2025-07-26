using FluentValidation;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Adicionar;

internal sealed class AdicionarAgendaCommandValidator : AbstractValidator<AdicionarAgendaCommand>
{
    public AdicionarAgendaCommandValidator()
    {
        RuleFor(x => x.Horario).NotNull();
        RuleFor(x => x.Horario.DataInicial).NotEmpty().NotNull();
        RuleFor(x => x.Horario.DataFinal).NotEmpty().NotNull();
        RuleFor(x => x.Descricao).NotEmpty().NotNull();

        RuleFor(x => x.Horario.DataInicial).LessThan(x => x.Horario.DataFinal);
    }
}
