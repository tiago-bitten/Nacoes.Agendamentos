using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;
using Nacoes.Agendamentos.ReadModels.Entities.Usuarios.Queries.RecuperarUsuarios;

namespace Nacoes.Agendamentos.API.Controllers.Usuarios;

public class UsuariosController : NacoesAuthenticatedController
{
    #region Adicionar
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromServices] IAdicionarUsuarioHandler handler,
                                               [FromBody] AdicionarUsuarioCommand command)
    {
        var resposta = await handler.ExecutarAsync(command);

        return Responder(resposta.Montar());
    }
    #endregion

    #region Recuperar
    [HttpGet]
    public async Task<IActionResult> Recuperar([FromServices] IRecuperarUsuarioQuery query,
                                               [FromQuery] RecuperarUsuarioParams @params)
    {
        var response = await query.ExecutarAsync(@params);

        return Responder(response.Items.Montar().ComTotal(response.Total)
                                                .DefinirProximaPagina(response.UltimoId, response.UltimaDataCriacao));
    }
    #endregion
}
