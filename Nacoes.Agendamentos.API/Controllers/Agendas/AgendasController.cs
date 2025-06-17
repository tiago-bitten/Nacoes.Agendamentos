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

    #region Recuperar Agendamentos
    [HttpGet("{agendaId:guid}/agendamentos")]
    public async Task<IActionResult> RecuperarAgendamentos()
    {
        throw new NotImplementedException();
    }
    #endregion
}
