using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Abstracts;

[Authorize]
public abstract class NacoesAuthenticatedController : NacoesController
{
    [Obsolete("Utilizar result.AsHttpResult()")]
    public override OkObjectResult Ok(object? value) => base.Ok(value);

    [Obsolete("Utilizar result.AsHttpResult()")]
    public override BadRequestObjectResult BadRequest(object? error) => base.BadRequest(error);
}
