using Domain.Shared.Entities;
using Domain.Shared.Results;

namespace Domain.Eventos.Suspensoes;

public sealed class EventSuspension : RemovableEntity
{
    private EventSuspension() { }

    private EventSuspension(DateOnly? endDate, EEventSuspensionStatus status)
    {
        EndDate = endDate;
        Status = status;
    }

    public Guid EventId { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public DateTimeOffset? CompletionDate { get; private set; }
    public EEventSuspensionStatus Status { get; private set; }

    internal static Result<EventSuspension> Create(DateOnly? endDate)
    {
        var today = DateOnly.FromDateTime(DateTimeOffset.UtcNow.DateTime);

        if (endDate == today)
        {
            return EventSuspensionErrors.EndDateCannotBeToday;
        }

        if (endDate < today)
        {
            return EventSuspensionErrors.EndDateCannotBeBeforeToday;
        }

        return new EventSuspension(endDate, EEventSuspensionStatus.Active);
    }

    internal Result Cancel()
    {
        if (Status is EEventSuspensionStatus.Cancelled)
        {
            return EventSuspensionErrors.NotAvailableToCancel;
        }

        Status = EEventSuspensionStatus.Cancelled;

        return Result.Success();
    }

    internal Result Close()
    {
        if (Status is not EEventSuspensionStatus.Active)
        {
            return EventSuspensionErrors.NotAvailableToClose;
        }

        Status = EEventSuspensionStatus.Closed;
        CompletionDate = DateTimeOffset.UtcNow;

        return Result.Success();
    }
}

public enum EEventSuspensionStatus
{
    Active = 0,
    Cancelled = 1,
    Closed = 2
}
