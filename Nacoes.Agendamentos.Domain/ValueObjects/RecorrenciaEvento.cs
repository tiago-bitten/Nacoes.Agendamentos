using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.ValueObjects;

public sealed record RecorrenciaEvento
{
    public Guid? Id { get; init; }
    public ETipoRecorrenciaEvento Tipo { get; init; }
    public int? Valor { get; init; }
    public DateOnly? DataFinal { get; init; }
    
    private RecorrenciaEvento() { }
    
    public RecorrenciaEvento(ETipoRecorrenciaEvento tipo, int? valor, DateOnly? dataFinal)
    {
        Tipo = tipo;
        
        if (tipo is ETipoRecorrenciaEvento.Nenhuma)
        {
            Id = null;
            Valor = null;
            DataFinal = null;
            return;
        }
        
        if (valor is null or <= 0)
        {
            throw new InvalidOperationException();
        }
        
        if (dataFinal.HasValue && dataFinal.Value < DateOnly.FromDateTime(DateTimeOffset.UtcNow.DateTime))
        {
            throw new InvalidOperationException();
        }
        
        Id = Guid.NewGuid();
        Valor = valor.Value;
        DataFinal = dataFinal;
    }
    
    public bool Equals(RecorrenciaEvento? other) =>
        other != null && Id == other.Id && Tipo == other.Tipo && Valor == other.Valor && DataFinal == other.DataFinal;

    public override int GetHashCode() =>
        HashCode.Combine(Id, Tipo, Valor, DataFinal);

    public override string ToString() =>
        $"{Id} {Tipo} {Valor} {DataFinal}";
}

public enum ETipoRecorrenciaEvento
{
    Nenhuma = 0,
    Diario = 1,
    Semanal = 2,
    Mensal = 3,
    Anual = 4
}

public static class RecorrenciaEventoErrors
{
    public static readonly Error ValorNaoPodeSerMenorOuIgualAZero =
        Error.Problem("RecorrenciaEvento.ValorNaoPodeSerMenorOuIgualAZero", "Valor da recorrencia deve ser maior que zero.");
    
    public static readonly Error DataFinalNaoPodeSerAnterioraDataHoje =
        Error.Problem("RecorrenciaEvento.DataFinalNaoPodeSerAnterioraDataHoje", "Data final da recorrencia deve ser posterior ou igual à data de hoje.");
}