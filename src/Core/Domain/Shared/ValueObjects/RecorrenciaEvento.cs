namespace Domain.Shared.ValueObjects;

public sealed record RecorrenciaEvento
{
    public Guid? Id { get; init; }
    public ETipoRecorrenciaEvento Tipo { get; init; }
    public int? Intervalo { get; init; }
    public DateOnly? DataFinal { get; init; }

    internal RecorrenciaEvento() { }

    public RecorrenciaEvento(ETipoRecorrenciaEvento tipo, int? intervalo, DateOnly? dataFinal)
    {
        Tipo = tipo;

        if (tipo is ETipoRecorrenciaEvento.Nenhuma)
        {
            Id = null;
            Intervalo = null;
            DataFinal = null;
            return;
        }

        if (intervalo is null or <= 0)
        {
            throw new InvalidOperationException();
        }

        if (dataFinal.HasValue && dataFinal.Value < DateOnly.FromDateTime(DateTimeOffset.UtcNow.DateTime))
        {
            throw new InvalidOperationException();
        }

        Id = Guid.NewGuid();
        Intervalo = intervalo.Value;
        DataFinal = dataFinal;
    }

    public bool Equals(RecorrenciaEvento? other) =>
        other != null && Id == other.Id && Tipo == other.Tipo && Intervalo == other.Intervalo && DataFinal == other.DataFinal;

    public override int GetHashCode() =>
        HashCode.Combine(Id, Tipo, Intervalo, DataFinal);

    public override string ToString() =>
        $"{Id} {Tipo} {Intervalo} {DataFinal}";
}

public enum ETipoRecorrenciaEvento
{
    Nenhuma = 0,
    Diario = 1,
    Semanal = 2,
    Mensal = 3,
    Anual = 4
}

public static class RecorrenciaEventoExtensions
{
    public static RecorrenciaEvento Copiar(this RecorrenciaEvento recorrenciaEvento)
        => new()
        {
            Id = recorrenciaEvento.Id,
            Tipo = recorrenciaEvento.Tipo,
            Intervalo = recorrenciaEvento.Intervalo,
            DataFinal = recorrenciaEvento.DataFinal
        };
}
