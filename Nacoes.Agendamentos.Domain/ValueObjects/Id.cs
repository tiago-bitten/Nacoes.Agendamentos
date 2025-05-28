namespace Nacoes.Agendamentos.Domain.ValueObjects;

public readonly struct Id<T> : IEquatable<Id<T>>
{
    public Guid Value { get; }

    public Id(string value)
    {
        if (Guid.TryParse(value, out var guidValue))
        {
            throw new ArgumentException("Id inválido.", nameof(value));
        }

        Value = guidValue;
    }

    public static Id<T> Novo() => new(Guid.NewGuid().ToString());

    public Guid ToGuid() => Value;

    public override string ToString() => Value.ToString();

    public bool Equals(Id<T> other) => Value.Equals(other.Value);

    public override bool Equals(object? obj) =>
        obj is Id<T> other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(typeof(T), Value);

    public static bool operator ==(Id<T> left, Id<T> right) => left.Equals(right);

    public static bool operator !=(Id<T> left, Id<T> right) => !(left == right);
}
