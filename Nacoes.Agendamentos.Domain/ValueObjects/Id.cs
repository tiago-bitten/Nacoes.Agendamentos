namespace Nacoes.Agendamentos.Domain.ValueObjects;

public sealed record class Id<T> : IEquatable<Id<T>>
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

    private Id(Guid value)
    {
        Value = value;
    }

    public static Id<T> Novo() => new Id<T>(Guid.NewGuid());

    public Guid ToGuid() => Value;

    public override string ToString() => Value.ToString();

    public bool Equals(Id<T>? other) => other is not null && Value.Equals(other.Value);

    public override int GetHashCode() => HashCode.Combine(typeof(T), Value);
}
