using System.Text.Json.Serialization;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Common.Responses;
public sealed record ApiResponse<T> where T : class
{
    public bool Sucesso { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Mensagem { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Dados { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Total { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Error? Erro { get; set; }
}