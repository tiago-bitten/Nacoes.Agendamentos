﻿using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgenda;

namespace Nacoes.Agendamentos.API.Controllers.Agendas;

public class AgendasController : NacoesController
{
    #region Adicionar
    public async Task<IActionResult> Adicionar([FromServices] IAdicionarAgendaHandler handler,
                                               [FromBody] AdicionarAgendaCommand command)
    {
        var resposta = await handler.ExecutarAsync(command);

        return Responder(resposta.Montar());
    }
    #endregion
}
