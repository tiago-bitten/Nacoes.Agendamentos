using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.API.Extensions;

public static class ApiResponseExtensions
{
    public static ObjectResult It<T>(this Result<T> result, string message) where T : class
    {
        ApiResponse<T> response;
        if (result.IsFailure)
        {
            response = new ApiResponse<T>
            {
                Sucesso = false,
                Erro = result.Error
            };
            
            return new ObjectResult(response)
            {
                StatusCode = 400,
            };
        }

        response = new ApiResponse<T>
        {
            Sucesso = true,
            Mensagem = message,
            Dados = result.Value,
            Erro = null
        };
        
        return new ObjectResult(response);
    }
}