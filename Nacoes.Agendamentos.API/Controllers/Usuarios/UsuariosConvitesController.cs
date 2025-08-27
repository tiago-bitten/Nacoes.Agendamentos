using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AceitarConvite;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarConvite;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.RecusarConvite;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Queries.RecuperarConvitesPorToken;

namespace Nacoes.Agendamentos.API.Controllers.Usuarios;

[Route("api/usuarios-convites")]
public sealed class UsuariosConvitesController : NacoesAuthenticatedController
{
    #region Adicionar
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromServices] ICommandHandler<AdicionarUsuarioConviteCommand, UsuarioConviteResponse> handler,
                                               [FromBody] AdicionarUsuarioConviteCommand command)
    {
        var result = await handler.Handle(command);
        
        return result.AsHttpResult(mensagem: "Convite enviado com sucesso.");
    }
    
    
    [HttpPost("ovo")]
    public IActionResult Post()
    {
        var auth = HttpContext.Request.Headers.Authorization.ToString();
        Console.WriteLine($"Header Authorization recebido: {auth}");
        return Ok(auth);
    }

    #endregion
    
    #region Aceitar
    [AllowAnonymous]
    [HttpPut("aceitar")]
    public async Task<IActionResult> Aceitar(
        ICommandHandler<AceitarUsuarioConviteCommand, AceitarUsuarioConviteResponse> handler,
        IAmbienteContext ambienteContext,
        [FromBody] AceitarUsuarioConviteCommand command)
    {
        ambienteContext.StartBotSession();
        
        var result = await handler.Handle(command);
        
        return result.AsHttpResult(mensagem: "Convite aceito com sucesso.");
    }
    #endregion
    
    #region Recusar
    [AllowAnonymous]
    [HttpPut("recusar")]
    public async Task<IActionResult> Recusar(ICommandHandler<RecusarUsuarioConviteCommand> handler,
                                             [FromBody] RecusarUsuarioConviteCommand command)
    {
        var result = await handler.Handle(command);
        
        return result.AsHttpResult(mensagem: "Convite recusado com sucesso.");
    }
    #endregion
    
    #region RecuperarPorToken
    [AllowAnonymous]
    [HttpGet("token/{token}")]    
    public async Task<IActionResult> RecuperarPorToken(IQueryHandler<RecuperarUsuarioConvitePorTokenQuery, RecuperarUsuarioConvitePorTokenResponse> handler,
                                                       string token)
    {
        var query = new RecuperarUsuarioConvitePorTokenQuery(token);
        
        var result = await handler.Handle(query);
        
        return result.AsHttpResult(mensagem: "Convite recuperado com sucesso.");
    }
    #endregion
}