namespace Domain.Shared.ValueObjects;

public sealed record EventRecurrence
{
    public Guid? Id { get; init; }
    public EEventRecurrenceType Type { get; init; }
    public int? Interval { get; init; }
    public DateOnly? EndDate { get; init; }

    internal EventRecurrence() { }

    public EventRecurrence(EEventRecurrenceType type, int? interval, DateOnly? endDate)
    {
        Type = type;

        if (type is EEventRecurrenceType.None)
        {
            Id = null;
            Interval = null;
            EndDate = null;
            return;
        }

        if (interval is null or <= 0)
        {
            throw new InvalidOperationException();
        }

        if (endDate.HasValue && endDate.Value < DateOnly.FromDateTime(DateTimeOffset.UtcNow.DateTime))
        {
            throw new InvalidOperationException();
        }

        Id = Guid.NewGuid();
        Interval = interval.Value;
        EndDate = endDate;
    }

    public bool Equals(EventRecurrence? other) =>
        other != null && Id == other.Id && Type == other.Type && Interval == other.Interval && EndDate == other.EndDate;

    public override int GetHashCode() =>
        HashCode.Combine(Id, Type, Interval, EndDate);

    public override string ToString() =>
        $"{Id} {Type} {Interval} {EndDate}";
}

public enum EEventRecurrenceType
{
    None = 0,
    Daily = 1,
    Weekly = 2,
    Monthly = 3,
    Yearly = 4
}

public static class EventRecurrenceExtensions
{
    public static EventRecurrence Copy(this EventRecurrence eventRecurrence)
        => new()
        {
            Id = eventRecurrence.Id,
            Type = eventRecurrence.Type,
            Interval = eventRecurrence.Interval,
            EndDate = eventRecurrence.EndDate
        };
}
