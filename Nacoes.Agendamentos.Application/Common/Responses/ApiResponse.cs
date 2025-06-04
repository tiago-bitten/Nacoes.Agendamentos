using Nacoes.Agendamentos.Application.Common.Results;
using System.Text.Json.Serialization;

namespace Nacoes.Agendamentos.Application.Common.Responses;
public sealed record ApiResponse<T> where T : class
{
    public bool Sucesso { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Mensagem { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Dados { get; set; } = default;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Total { get; set; } = default;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UltimoId { get; set; } = default;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? UltimaDataCriacao { get; set; } = default;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Error? Erro { get; set; } = default;
}