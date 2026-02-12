using Microsoft.AspNetCore.Mvc;
using Application.Shared.Pagination;
using Application.Common.Responses;
using Domain.Shared.Results;

namespace API.Extensions;

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

    public static ObjectResult AsHttpResult<T>(this Result<PagedResponse<T>> result, string mensagem)
    {
        var response = result.IsSuccess
            ? ApiResponse.Ok(result.Value!, mensagem)
            : ApiResponse.Erro<T>(result.Error);

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
