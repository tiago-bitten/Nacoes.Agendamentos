using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Authentication.Commands.LoginExterno;
using Nacoes.Agendamentos.Application.Common.Responses;

namespace Nacoes.Agendamentos.API.Controllers.Authentication;

public class AuthController : NacoesController
{
    #region Login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromServices] ICommandHandler<LoginCommand, LoginResponse> handler,
                                           [FromBody] LoginCommand command)
    {
        var result = await handler.Handle(command);

        return result.AsHttpResult(mensagem: "Login realizado com sucesso.");
    }
    #endregion
    
    #region Login
    [HttpPost("login-externo")]
    public async Task<IActionResult> Login([FromServices] ICommandHandler<LoginExternoCommand> handler,
                                           [FromBody] LoginExternoCommand command)
    {
        var result = await handler.Handle(command);

        return result.AsHttpResult(mensagem: "Login realizado com sucesso.");
    }
    #endregion
}
