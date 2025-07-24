using Microsoft.AspNetCore.Mvc;

namespace Nacoes.Agendamentos.API.Controllers.Abstracts;

//[Authorize]
public abstract class NacoesAuthenticatedController : NacoesController
{
    [Obsolete("Utilizar result.AsHttpResult()")]
    public override OkObjectResult Ok(object? value) => base.Ok(value);
    
    [Obsolete("Utilizar result.AsHttpResult()")]
    public override BadRequestObjectResult BadRequest(object? error) => base.BadRequest(error);
}
