namespace Domain.Shared.ValueObjects;

public sealed record Schedule
{
    public DateTimeOffset StartDate { get; }
    public DateTimeOffset EndDate { get; }

    public int DurationInSeconds => (int)(EndDate - StartDate).TotalSeconds;

    public Schedule(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        if (startDate < DateTime.UtcNow)
        {
            throw new ArgumentException("Start date cannot be in the past.", nameof(startDate));
        }

        if (endDate <= startDate)
        {
            throw new ArgumentException("End date must be after the start date.", nameof(endDate));
        }

        StartDate = startDate;
        EndDate = endDate;
    }

    public bool Equals(Schedule? other) =>
        other != null && StartDate.Equals(other.StartDate) && Nullable.Equals(EndDate, other.EndDate);

    public override int GetHashCode() =>
        HashCode.Combine(StartDate, EndDate);

    public override string ToString()
    {
        var finalStr = $" to {EndDate:dd/MM/yyyy HH:mm}";
        return $"{StartDate:dd/MM/yyyy HH:mm}{finalStr}";
    }

    public static bool operator >(Schedule left, Schedule right) => left.StartDate > right.StartDate;
    public static bool operator <(Schedule left, Schedule right) => left.StartDate < right.StartDate;
    public static bool operator >=(Schedule left, Schedule right) => left.StartDate >= right.StartDate;
    public static bool operator <=(Schedule left, Schedule right) => left.StartDate <= right.StartDate;
}
