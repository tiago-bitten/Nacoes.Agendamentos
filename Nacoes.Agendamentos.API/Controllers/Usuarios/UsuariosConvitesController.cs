using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AceitarConvite;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.EnviarConvite;

namespace Nacoes.Agendamentos.API.Controllers.Usuarios;

[Route("api/usuarios-convites")]
public sealed class UsuariosConvitesController : NacoesAuthenticatedController
{
    #region Enviar
    [HttpPost]
    public async Task<IActionResult> Enviar([FromServices] ICommandHandler<EnviarUsuarioConviteCommand, string> handler,
                                            [FromBody] EnviarUsuarioConviteCommand command)
    {
        var result = await handler.Handle(command);

        return Ok(result);
    }
    #endregion
    
    #region Aceitar
    [HttpPost("aceitar")]
    public async Task<IActionResult> Aceitar([FromServices] ICommandHandler<AceitarUsuarioConviteCommand, AceitarUsuarioConviteResponse> handler,
                                             [FromBody] AceitarUsuarioConviteCommand command)
    {
        var result = await handler.Handle(command);

        return Ok(result);
    }
    #endregion
}