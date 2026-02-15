using FluentValidation;

namespace Application.Entities.Eventos.Commands.Reservas.AdicionarReserva;

internal sealed class AddReservationCommandValidator : AbstractValidator<AddReservationCommand>
{
    public AddReservationCommandValidator()
    {
        RuleFor(x => x.VolunteerMinistryId).NotEmpty();
        RuleFor(x => x.ActivityId).NotEmpty();
    }
}
