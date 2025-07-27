using System.Text.Json.Serialization;
using Nacoes.Agendamentos.Application.Common.Pagination;
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
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Cursor { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HasNext { get; set; }
}

public static class ApiResponse
{
    public static ApiResponse<object> Ok(object value, string mensagem) => new()
    {
        Sucesso = true, 
        Dados = value, 
        Erro = null, 
        Total = null, 
        Cursor = null,
        HasNext = null,
        Mensagem = mensagem
    };
    
    public static ApiResponse<object> Ok(string mensagem) => new()
    {
        Sucesso = true, 
        Dados = null, 
        Erro = null, 
        Total = null, 
        Cursor = null,
        HasNext = null,
        Mensagem = mensagem
    };
    
    public static ApiResponse<List<T>> Ok<T>(PagedResponse<T> paged, string mensagem) => new()
    {
        Sucesso = true,
        Dados = paged.Items,
        Total = paged.Total,
        Cursor = paged.Cursor,
        HasNext = paged.HasNext,
        Mensagem = mensagem
    };
    
    public static ApiResponse<List<T>> Erro<T>(Error error) => new()
    {
        Sucesso = false, 
        Dados = null, 
        Erro = error, 
        Total = null, 
        Cursor = null,
        HasNext = null,
        Mensagem = string.Empty
    };
    
    public static ApiResponse<object> Erro(Error error) => new()
    {
        Sucesso = false, 
        Dados = null, 
        Erro = error, 
        Total = null, 
        Cursor = null,
        HasNext = null,
        Mensagem = string.Empty
    };
}