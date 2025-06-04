using Nacoes.Agendamentos.Application.Common.Results;

namespace Nacoes.Agendamentos.Application.Common.Responses;

public static class ApiResponseExtensions
{
    public static ApiResponse<T> Montar<T>(this Result<T, Error> resultado, string? mensagem = default) where T : class
    {
        return resultado.Match(
            sucesso => new ApiResponse<T>
            {
                Sucesso = true,
                Mensagem = mensagem ?? "Operação realizada com sucesso",
                Dados = sucesso
            },
            erro => new ApiResponse<T>
            {
                Sucesso = false,
                Mensagem = mensagem ?? "Ocorreu um erro ao realizar a operação",
                Erro = erro
            });
    }

    public static ApiResponse<T> Montar<T>(this T lista, string? mensagem = default) where T : class
    {
        return new ApiResponse<T>
        {
            Sucesso = true,
            Mensagem = mensagem ?? "Listagem realizada com sucesso.",
            Dados = lista
        };
    }

    public static ApiResponse<T> ComTotal<T>(this ApiResponse<T> resposta, int total) where T : class
    {
        resposta.Total = total;
        return resposta;
    }

    public static ApiResponse<T> DefinirProximaPagina<T>(this ApiResponse<T> resposta, string? ultimoId, DateTime? ultimaDataCriacao) where T : class
    {
        resposta.UltimoId = ultimoId;
        resposta.UltimaDataCriacao = ultimaDataCriacao;
        return resposta;
    }
}