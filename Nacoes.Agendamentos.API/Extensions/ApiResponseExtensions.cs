using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.API.Extensions;

public static class ApiResponseExtensions
{
    public static ObjectResult AsHttpResult<T>(this Result<T> result, string mensagem)
    {
        var response = result.IsFailure
            ? ApiResponse.Erro(result.Error)
            : ApiResponse.Ok(result.Value!, mensagem);

        return new ObjectResult(response)
        {
            StatusCode = result.GetStatusCode
        };
    }

    public static ObjectResult AsHttpResult(this Result result, string mensagem)
    {
        var response = result.IsFailure
            ? ApiResponse.Erro(result.Error)
            : ApiResponse.Ok(mensagem);

        return new ObjectResult(response)
        {
            StatusCode = result.GetStatusCode
        };
    }
}
