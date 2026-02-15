using Domain.Shared.Entities;
using Domain.Shared.Results;

namespace Domain.Eventos.Reservas;

public sealed class Reservation : RemovableEntity
{
    private Reservation()
    {
    }

    private Reservation(
        Guid volunteerMinistryId,
        Guid activityId,
        EReservationStatus status,
        EReservationOrigin origin)
    {
        VolunteerMinistryId = volunteerMinistryId;
        ActivityId = activityId;
        Status = status;
        Origin = origin;
    }

    public Guid EventId { get; private set; }
    public Guid VolunteerMinistryId { get; private set; }
    public Guid ActivityId { get; private set; }
    public EReservationStatus Status { get; private set; }
    public EReservationOrigin Origin { get; private set; }

    public Event Event { get; private set; } = null!;

    internal static Result<Reservation> Create(Guid volunteerMinistryId, Guid activityId, EReservationOrigin origin)
    {
        return new Reservation(volunteerMinistryId, activityId, EReservationStatus.Confirmed, origin);
    }

    internal Result Cancel()
    {
        if (Status is not (EReservationStatus.Confirmed or EReservationStatus.AwaitingConfirmation))
        {
            return ReservationErrors.NotReserved;
        }

        Status = EReservationStatus.Cancelled;

        return Result.Success();
    }
}

public enum EReservationStatus
{
    AwaitingConfirmation = 0,
    Confirmed = 1,
    Cancelled = 2
}

public enum EReservationOrigin
{
    Manual = 0,
    Automatic = 1,
    Roster = 2
}
