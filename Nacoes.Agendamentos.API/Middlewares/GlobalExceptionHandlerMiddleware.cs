using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Domain.Exceptions;
using System.Text.Json;

namespace Nacoes.Agendamentos.API.Middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DomainException ex)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";

            var baseResponse = new ApiResponse<object>()
            {
                Sucesso = false,
                Mensagem = "Ocorreu um erro ao processar a requisição",
                Erro = new Error(ex.Code, ex.Message)
            };

            await context.Response.WriteAsJsonAsync(baseResponse, PascalCaseOptions);
        }

        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var baseResponse = new ApiResponse<object>()
            {
                Sucesso = false,
                Mensagem = "Ocorreu um erro interno no servidor",
                Erro = new Error("Interno", ex.Message)
            };

            await context.Response.WriteAsJsonAsync(baseResponse, PascalCaseOptions);
        }
    }

    private static JsonSerializerOptions PascalCaseOptions => new()
    {
        PropertyNamingPolicy = null
    };
}
