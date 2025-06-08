using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgenda;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;

namespace Nacoes.Agendamentos.API.Controllers.Agendas;

public class AgendasController : NacoesController
{
    #region Adicionar
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromServices] IAdicionarAgendaHandler handler,
                                               [FromBody] AdicionarAgendaCommand command)
    {
        var resposta = await handler.ExecutarAsync(command);

        return Responder(resposta.Montar());
    }
    #endregion

    #region Agendar
    [HttpPost("{agendaId:guid}/agendar")]
    public async Task<IActionResult> Agendar([FromServices] IAgendarHandler handler,
                                             [FromBody] AgendarCommand command,
                                             [FromRoute] Guid agendaId)
    {
        var resposta = await handler.ExcutarAsync(command, agendaId);

        return Responder(resposta.Montar());
    }
    #endregion
}
