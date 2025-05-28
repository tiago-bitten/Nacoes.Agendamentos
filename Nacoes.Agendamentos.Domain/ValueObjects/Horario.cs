namespace Nacoes.Agendamentos.Domain.ValueObjects;

public readonly struct Horario : IEquatable<Horario>
{
    public DateTime DataInicial { get; }
    public DateTime? DataFinal { get; }

    public int? DuracaoEmSegundos =>
        DataFinal.HasValue ? (int)(DataFinal.Value - DataInicial).TotalSeconds : null;

    public Horario(DateTime dataInicial, DateTime? dataFinal = null)
    {
        if (dataInicial < DateTime.UtcNow)
        {
            throw new ArgumentException("Data inicial não pode estar no passado.", nameof(dataInicial));
        }

        if (dataFinal.HasValue && dataFinal <= dataInicial)
        {
            throw new ArgumentException("Data final deve ser posterior à data inicial.", nameof(dataFinal));
        }

        DataInicial = dataInicial;
        DataFinal = dataFinal;
    }

    public bool Equals(Horario other) =>
        DataInicial.Equals(other.DataInicial) && Nullable.Equals(DataFinal, other.DataFinal);

    public override bool Equals(object? obj) =>
        obj is Horario other && Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(DataInicial, DataFinal);

    public override string ToString()
    {
        var finalStr = DataFinal.HasValue ? $" até {DataFinal:dd/MM/yyyy HH:mm}" : "";
        return $"{DataInicial:dd/MM/yyyy HH:mm}{finalStr}";
    }

    public static bool operator ==(Horario left, Horario right) => left.Equals(right);
    public static bool operator !=(Horario left, Horario right) => !(left == right);
    public static bool operator >(Horario left, Horario right) => left.DataInicial > right.DataInicial;
    public static bool operator <(Horario left, Horario right) => left.DataInicial < right.DataInicial;
    public static bool operator >=(Horario left, Horario right) => left.DataInicial >= right.DataInicial;
    public static bool operator <=(Horario left, Horario right) => left.DataInicial <= right.DataInicial;

}
