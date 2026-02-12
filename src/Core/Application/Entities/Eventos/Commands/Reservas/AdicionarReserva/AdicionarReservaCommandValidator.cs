using FluentValidation;

namespace Application.Entities.Eventos.Commands.Reservas.AdicionarReserva;

internal sealed class AdicionarReservaCommandValidator : AbstractValidator<AdicionarReservaCommand>
{
    public AdicionarReservaCommandValidator()
    {
        RuleFor(x => x.VoluntarioMinisterioId).NotEmpty();
        RuleFor(x => x.AtividadeId).NotEmpty();
    }
}
