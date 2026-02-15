using Application.Shared.Messaging;

namespace Application.Entities.Eventos.Commands.Reservas.AdicionarReserva;

public sealed record AddReservationCommand : ICommand<Guid>
{
    public Guid EventId { get; init; }
    public Guid VolunteerMinistryId { get; init; }
    public Guid ActivityId { get; init; }
}
