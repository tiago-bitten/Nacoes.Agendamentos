namespace Domain.Shared.ValueObjects;

public sealed record BirthDate
{
    public DateOnly Value { get; }
    public int Age => CalculateAge(Value);
    public bool IsMinor => Age < 18;

    public BirthDate(DateOnly value)
    {
        if (value == DateOnly.FromDateTime(DateTime.Today))
        {
            throw new ArgumentException("Birth date cannot be today.", nameof(value));
        }

        if (value > DateOnly.FromDateTime(DateTime.Today))
        {
            throw new ArgumentException("Birth date cannot be in the future.", nameof(value));
        }

        var age = CalculateAge(value);
        if (age > 140)
        {
            throw new ArgumentException("Age cannot exceed 140 years.", nameof(value));
        }

        Value = value;
    }

    private static int CalculateAge(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }

    public override string ToString() => Value.ToString("dd/MM/yyyy");

    public bool Equals(BirthDate? other) => other != null && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
}
