using FluentValidation;

namespace Nacoes.Agendamentos.Application.Entities.Eventos.Commands.Adicionar;

internal sealed class AdicionarEventoCommandValidator : AbstractValidator<AdicionarEventoCommand>
{
    public AdicionarEventoCommandValidator()
    {
        RuleFor(x => x.Horario).NotNull();
        RuleFor(x => x.Horario.DataInicial).NotEmpty().NotNull();
        RuleFor(x => x.Horario.DataFinal).NotEmpty().NotNull();
        RuleFor(x => x.Descricao).NotEmpty().NotNull();

        RuleFor(x => x.Horario.DataInicial).LessThan(x => x.Horario.DataFinal);
    }
}
