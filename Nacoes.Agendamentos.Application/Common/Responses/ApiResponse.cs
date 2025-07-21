using System.Text.Json.Serialization;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Common.Responses;

public sealed record ApiResponse<T>
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

public static class ApiResponse
{
    public static ApiResponse<object> Ok(object value, string mensagem) => new()
    {
        Sucesso = true, 
        Dados = value, 
        Erro = null, 
        Total = null, 
        Mensagem = mensagem
    };
    
    public static ApiResponse<object> Ok(string mensagem) => new()
    {
        Sucesso = true, 
        Dados = null, 
        Erro = null, 
        Total = null, 
        Mensagem = mensagem
    };
    
    public static ApiResponse<object> Erro(Error error) => new()
    {
        Sucesso = false, 
        Dados = null, 
        Erro = error, 
        Total = null, 
        Mensagem = string.Empty
    };
}