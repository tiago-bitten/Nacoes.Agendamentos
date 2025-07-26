using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AceitarConvite;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarConvite;

namespace Nacoes.Agendamentos.API.Controllers.Usuarios;

[Route("api/usuarios-convites")]
public sealed class UsuariosConvitesController : NacoesAuthenticatedController
{
    #region Adicionar
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromServices] ICommandHandler<AdicionarUsuarioConviteCommand, UsuarioConviteResponse> handler,
                                               [FromBody] AdicionarUsuarioConviteCommand command)
    {
        var result = await handler.Handle(command);
        return result.AsHttpResult(mensagem: "Convite enviado com sucesso.");
    }
    #endregion
    
    #region Aceitar
    [HttpPost("aceitar")]
    public async Task<IActionResult> Aceitar([FromServices] ICommandHandler<AceitarUsuarioConviteCommand, AceitarUsuarioConviteResponse> handler,
                                             [FromBody] AceitarUsuarioConviteCommand command)
    {
        var result = await handler.Handle(command);

        return result.AsHttpResult(mensagem: "Convite aceito com sucesso.");
    }
    #endregion
}