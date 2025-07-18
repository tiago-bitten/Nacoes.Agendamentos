using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.API.Controllers.Abstracts;

[Route("api/[controller]")]
[ApiController]
public abstract class NacoesController : ControllerBase
{
    protected ObjectResult Responder<T>(ApiResponse<T> resposta) where T : class
    {
        var statusCode = resposta.Sucesso ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
        var objectResult = new ObjectResult(resposta)
        {
            StatusCode = statusCode
        };

        return objectResult;
    }
    
    protected ObjectResult Responder<T>(Result<T> result) where T : class
    {
        var statusCode = result.IsSuccess ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
        var objectResult = new ObjectResult(result.Value)
        {
            StatusCode = statusCode
        };

        return objectResult;
    }
}
