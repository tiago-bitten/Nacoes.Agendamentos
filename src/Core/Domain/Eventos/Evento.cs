using Domain.Shared.Entities;
using Domain.Shared.Results;
using Domain.Eventos.DomainEvents;
using Domain.Eventos.Reservas;
using Domain.Eventos.Suspensoes;
using Domain.Shared.ValueObjects;

namespace Domain.Eventos;

public sealed class Event : RemovableEntity, IAggregateRoot
{
    public const int DescriptionMaxLength = 200;

    private readonly List<Reservation> _reservations = [];
    private readonly List<EventSuspension> _suspensions = [];

    private Event() { }

    private Event(
        string description,
        Schedule schedule,
        EEventStatus status,
        EventRecurrence recurrence,
        int? maxReservationCount)
    {
        Description = description;
        Schedule = schedule;
        Status = status;
        Recurrence = recurrence;
        MaxReservationCount = maxReservationCount;
    }

    public string Description { get; private set; } = string.Empty;
    public Schedule Schedule { get; private set; } = null!;
    public int ReservationCount { get; private set; }
    public int? MaxReservationCount { get; private set; }
    public EEventStatus Status { get; private set; }
    public EventRecurrence Recurrence { get; private set; } = null!;

    public IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();
    public IReadOnlyCollection<EventSuspension> Suspensions => _suspensions.AsReadOnly();

    public static Result<Event> Create(
        string description,
        Schedule schedule,
        EventRecurrence recurrence,
        int? maxReservationCount)
    {
        description = description.Trim();

        if (string.IsNullOrWhiteSpace(description))
        {
            return EventErrors.DescriptionRequired;
        }

        if (maxReservationCount is < 1)
        {
            return EventErrors.InvalidMaxReservationCount;
        }

        var @event = new Event(description, schedule, EEventStatus.Open, recurrence, maxReservationCount);

        @event.Raise(new EventAddedDomainEvent(@event.Id));

        return @event;
    }

    public Result UpdateSchedule(Schedule schedule)
    {
        if (Status is not (EEventStatus.Open or EEventStatus.Full or EEventStatus.Suspended))
        {
            return EventErrors.NotAvailableToUpdateSchedule;
        }

        Schedule = schedule;

        return Result.Success();
    }

    public Result UpdateRecurrence(EventRecurrence recurrence)
    {
        if (Status is not (EEventStatus.Open or EEventStatus.Full or EEventStatus.Suspended))
        {
            return EventErrors.NotAvailableToUpdateRecurrence;
        }

        Recurrence = recurrence;

        return Result.Success();
    }

    public Result Cancel()
    {
        if (Status is not (EEventStatus.Open or EEventStatus.Full or EEventStatus.Suspended))
        {
            return EventErrors.NotAvailableToCancel;
        }

        var reservationsToCancel = _reservations
            .Where(x => x.Status is EReservationStatus.Confirmed or EReservationStatus.AwaitingConfirmation);

        foreach (var reservation in reservationsToCancel)
        {
            var cancelResult = reservation.Cancel();
            if (cancelResult.IsFailure)
            {
                return cancelResult.Error;
            }
        }

        Status = EEventStatus.Cancelled;

        return Result.Success();
    }

    public Result Suspend(DateOnly? endDate)
    {
        if (Status is not (EEventStatus.Open or EEventStatus.Full))
        {
            return EventErrors.NotAvailableToSuspend;
        }

        var suspensionResult = EventSuspension.Create(endDate);
        if (suspensionResult.IsFailure)
        {
            return suspensionResult.Error;
        }

        var suspension = suspensionResult.Value;
        _suspensions.Add(suspension);

        return Result.Success();
    }

    public Result<Reservation> CreateReservation(Guid volunteerMinistryId, Guid activityId, EReservationOrigin origin)
    {
        var reservationResult = Reservation.Create(volunteerMinistryId, activityId, origin);
        if (reservationResult.IsFailure)
        {
            return reservationResult.Error;
        }

        var reservation = reservationResult.Value;
        _reservations.Add(reservation);

        return reservation;
    }

    public Result CancelReservation(Guid reservationId)
    {
        if (Status is not (EEventStatus.Open or EEventStatus.Full or EEventStatus.Suspended))
        {
            return EventErrors.NotAvailableToCancelReservation;
        }

        var reservation = _reservations.FirstOrDefault(a => a.Id == reservationId);
        if (reservation is null)
        {
            return ReservationErrors.NotFound;
        }

        return reservation.Cancel();
    }
}

public enum EEventStatus
{
    Open = 0,
    Cancelled = 1,
    Closed = 2,
    Full = 3,
    Suspended = 4
}
