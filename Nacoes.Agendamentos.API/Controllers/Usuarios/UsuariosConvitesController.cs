using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.EnviarConvite;

namespace Nacoes.Agendamentos.API.Controllers.Usuarios;

[Route("api/usuarios-convites")]
public sealed class UsuariosConvitesController : NacoesAuthenticatedController
{
    #region Enviar
    [HttpPost]
    public async Task<IActionResult> Enviar([FromServices] ICommandHandler<EnviarUsuarioConviteCommand, EnviarUsuarioConviteResponse> handler,
                                            [FromBody] EnviarUsuarioConviteCommand command)
    {
        var resposta = await handler.Handle(command);

        return Ok(resposta);
    }
    #endregion
    
    #region Aceitar
    [HttpPost("aceitar")]
    public async Task<IActionResult> Aceitar([FromServices] ICommandHandler<EnviarUsuarioConviteCommand, EnviarUsuarioConviteResponse> handler,
                                             [FromBody] EnviarUsuarioConviteCommand command)
    {
        var resposta = await handler.Handle(command);

        return Ok(resposta);
    }
    #endregion
}