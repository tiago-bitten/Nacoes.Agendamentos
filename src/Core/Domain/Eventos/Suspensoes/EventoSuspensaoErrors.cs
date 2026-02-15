using Domain.Shared.Results;

namespace Domain.Eventos.Suspensoes;

public static class EventSuspensionErrors
{
    public static readonly Error EndDateCannotBeBeforeToday =
        Error.Problem(
            "event_suspension.end_date_cannot_be_before_today",
            "The suspension end date cannot be before today.");

    public static readonly Error EndDateCannotBeToday =
        Error.Problem("event_suspension.end_date_cannot_be_today", "The suspension end date cannot be today.");

    public static readonly Error NotAvailableToClose =
        Error.Problem(
            "event_suspension.not_available_to_close",
            "The suspension is not available to close.");

    public static readonly Error NotAvailableToCancel =
        Error.Problem("event_suspension.not_available_to_cancel", "The suspension has already been cancelled.");
}
