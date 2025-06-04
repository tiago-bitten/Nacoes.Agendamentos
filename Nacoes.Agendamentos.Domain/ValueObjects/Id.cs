namespace Nacoes.Agendamentos.Domain.ValueObjects;

public sealed record class Id<T> : IEquatable<Id<T>>, IComparable<Id<T>>
{
    public Guid Value { get; }

    public Id(string value)
    {
        if (!Guid.TryParse(value, out var guidValue))
        {
            throw new ArgumentException("Id inválido.", nameof(value));
        }

        Value = guidValue;
    }

    public Id(Guid value)
    {
        Value = value;
    }

    public static Id<T> Novo() => new(Guid.NewGuid());

    public Guid ToGuid() => Value;

    public override string ToString() => Value.ToString();

    public bool Equals(Id<T>? other) => other is not null && Value.Equals(other.Value);

    public override int GetHashCode() => HashCode.Combine(typeof(T), Value);

    public static implicit operator Id<T>(Guid value) => new(value);
    public static implicit operator Id<T>(string value) => new(value);

    public static implicit operator string(Id<T> value) => value.ToString();

    public int CompareTo(Id<T>? other)
    {
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }
}
