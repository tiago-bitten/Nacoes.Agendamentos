using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarMinisterio;

namespace Nacoes.Agendamentos.API.Controllers.Ministerios;
public class MinisteriosController : NacoesAuthenticatedController
{
    #region Adicionar
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromServices] IAdicionarMinisterioHandler handler,
                                               [FromBody] AdicionarMinisterioCommand command)
    {
        var resultado = await handler.ExecutarAsync(command);

        return Responder(resultado.Montar());
    }
    #endregion

    #region AdicionarAtividade
    [HttpPost("{ministerioId:guid}/atividades")]
    public async Task<IActionResult> AdicionarAtividade([FromServices] IAdicionarAtivdadeHandler handler,
                                                        [FromBody] AdicionarAtividadeCommand command,
                                                        [FromRoute] Guid ministerioId)
    {
        var resultado = await handler.ExecutarAsync(command, ministerioId);

        return Responder(resultado.Montar());
    }
    #endregion
}
