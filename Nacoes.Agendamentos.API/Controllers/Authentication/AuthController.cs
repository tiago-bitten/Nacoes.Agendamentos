using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Common.Responses;

namespace Nacoes.Agendamentos.API.Controllers.Authentication;

public class AuthController : NacoesController
{
    #region Login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromServices] ICommandHandler<LoginCommand, LoginResponse> handler,
                                           [FromBody] LoginCommand command)
    {
        var resposta = await handler.Handle(command);

        return Ok(resposta);
    }
    #endregion
}
