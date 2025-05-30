using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.Application.Common.Responses;

namespace Nacoes.Agendamentos.API.Controllers.Abstracts;

public abstract class NacoesController : ControllerBase
{
    protected IActionResult Responder<T>(ApiResponse<T> resposta) where T : class
    {
        var statusCode = resposta.Sucesso ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
        var objectResult = new ObjectResult(resposta)
        {
            StatusCode = statusCode
        };

        return objectResult;
    }
}
