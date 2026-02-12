using FluentValidation;
using Domain.Shared.ValueObjects;

namespace Application.Entities.Eventos.Commands.Adicionar;

internal sealed class AdicionarEventoCommandValidator : AbstractValidator<AdicionarEventoCommand>
{
    public AdicionarEventoCommandValidator()
    {
        RuleFor(x => x.Horario)
            .NotNull();

        RuleFor(x => x.Horario.DataInicial)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Horario.DataFinal)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Horario.DataInicial)
            .LessThan(x => x.Horario.DataFinal);

        RuleFor(x => x.Recorrencia.Tipo)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Recorrencia.Intervalo)
            .NotEmpty()
            .NotNull()
            .When(x => x.Recorrencia.Tipo is not ETipoRecorrenciaEvento.Nenhuma);

        RuleFor(x => x.Recorrencia.DataFinal)
            .NotEmpty()
            .NotNull()
            .When(x => x.Recorrencia.Tipo is not ETipoRecorrenciaEvento.Nenhuma);

        RuleFor(x => x.QuantidadeMaximaReservas)
            .GreaterThan(0)
            .When(x => x.QuantidadeMaximaReservas.HasValue);
    }
}
