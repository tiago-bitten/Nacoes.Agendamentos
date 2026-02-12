using Application.Common.Responses;
using System.Text.Json;
using Domain.Shared.Results;

namespace API.Middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var baseResponse = new ApiResponse<object>
            {
                Sucesso = false,
                Mensagem = "Ocorreu um erro interno no servidor",
                Erro = Error.Failure("Interno", ex.Message)
            };

            await context.Response.WriteAsJsonAsync(baseResponse, PascalCaseOptions);
        }
    }

    private static JsonSerializerOptions PascalCaseOptions => new()
    {
        PropertyNamingPolicy = null
    };
}
