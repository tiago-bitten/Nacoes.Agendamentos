namespace Nacoes.Agendamentos.ReadModels.Abstracts;
public record BaseQueryListResponse
{
    public int Total { get; set; }
    public string? UltimoId { get; set; }
    public DateTime? UltimaDataCriacao { get; set; }
}
