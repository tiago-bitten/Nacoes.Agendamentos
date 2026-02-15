using Domain.Shared.Results;

namespace Domain.Eventos;

public static class EventErrors
{
    public static readonly Error DescriptionRequired =
        Error.Problem("event.description_required", "The event description is required.");

    public static readonly Error ReservationAlreadyExists =
        Error.Problem("event.reservation_already_exists", "A reservation already exists for this volunteer in this event.");

    public static readonly Error NotAvailableToCancelReservation =
        Error.Problem(
            "event.not_available_to_cancel_reservation",
            "The event is not available to cancel the reservation.");

    public static readonly Error NotAvailableToOpen =
        Error.Problem("event.not_available_to_open", "The event is not available to open.");

    public static readonly Error NotAvailableToSuspend =
        Error.Problem("event.not_available_to_suspend", "The event is not available to suspend.");

    public static readonly Error NotAvailableToCancel =
        Error.Problem("event.not_available_to_cancel", "The event is not available to cancel.");

    public static readonly Error NotAvailableToUpdateSchedule =
        Error.Problem(
            "event.not_available_to_update_schedule",
            "The event is not available to update the schedule.");

    public static readonly Error NotAvailableToUpdateRecurrence =
        Error.Problem(
            "event.not_available_to_update_recurrence",
            "The event is not available to update the recurrence.");

    public static readonly Error InvalidMaxReservationCount =
        Error.Problem(
            "event.invalid_max_reservation_count",
            "The maximum reservation count must be greater than zero.");
}
