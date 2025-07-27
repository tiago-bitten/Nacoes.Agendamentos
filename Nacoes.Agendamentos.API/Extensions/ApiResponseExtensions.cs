using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.API.Extensions;

internal static class ApiResponseExtensions
{
    public static ObjectResult AsHttpResult<T>(this Result<T> result, string mensagem)
    {
        var response = result.IsSuccess
            ? ApiResponse.Ok(result.Value!, mensagem)
            : ApiResponse.Erro(result.Error);

        return new ObjectResult(response)
        {
            StatusCode = result.StatusCode
        };
    }

    public static ObjectResult AsHttpResult(this Result result, string mensagem)
    {
        var response = result.IsSuccess
            ? ApiResponse.Ok(mensagem)
            : ApiResponse.Erro(result.Error);

        return new ObjectResult(response)
        {
            StatusCode = result.StatusCode
        };
    }
}
