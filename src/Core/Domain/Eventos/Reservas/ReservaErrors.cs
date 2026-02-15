using Domain.Shared.Results;

namespace Domain.Eventos.Reservas;

public static class ReservationErrors
{
    public static readonly Error NotReserved =
        Error.Problem(
            "reservation.status_does_not_allow_cancellation",
            "Only reservations with status 'Confirmed' or 'AwaitingConfirmation' can be cancelled.");

    public static readonly Error NotFound =
        Error.Problem(
            "reservation.not_found",
            "Reservation not found.");
}
