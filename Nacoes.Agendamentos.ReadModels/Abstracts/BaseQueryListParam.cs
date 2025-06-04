namespace Nacoes.Agendamentos.ReadModels.Abstracts;

public record BaseQueryListParam
{
    public int Take { get; set; } = 10;
    public string? UltimoId { get; set; }
    public DateTime? UltimaDataCriacao { get; set; }
}
