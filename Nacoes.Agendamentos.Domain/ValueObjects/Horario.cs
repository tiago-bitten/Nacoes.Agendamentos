namespace Nacoes.Agendamentos.Domain.ValueObjects;

public sealed record Horario
{
    public DateTimeOffset DataInicial { get; }
    public DateTimeOffset DataFinal { get; }

    public int DuracaoEmSegundos => (int)(DataFinal - DataInicial).TotalSeconds;

    public Horario(DateTimeOffset dataInicial, DateTimeOffset dataFinal)
    {
        if (dataInicial < DateTime.UtcNow)
        {
            throw new ArgumentException("Data inicial não pode estar no passado.", nameof(dataInicial));
        }

        if (dataFinal <= dataInicial)
        {
            throw new ArgumentException("Data final deve ser posterior à data inicial.", nameof(dataFinal));
        }

        DataInicial = dataInicial;
        DataFinal = dataFinal;
    }

    public bool Equals(Horario? other) =>
        other != null && DataInicial.Equals(other.DataInicial) && Nullable.Equals(DataFinal, other.DataFinal);

    public override int GetHashCode() =>
        HashCode.Combine(DataInicial, DataFinal);

    public override string ToString()
    {
        var finalStr = $" até {DataFinal:dd/MM/yyyy HH:mm}";
        return $"{DataInicial:dd/MM/yyyy HH:mm}{finalStr}";
    }

    public static bool operator >(Horario left, Horario right) => left.DataInicial > right.DataInicial;
    public static bool operator <(Horario left, Horario right) => left.DataInicial < right.DataInicial;
    public static bool operator >=(Horario left, Horario right) => left.DataInicial >= right.DataInicial;
    public static bool operator <=(Horario left, Horario right) => left.DataInicial <= right.DataInicial;

}
