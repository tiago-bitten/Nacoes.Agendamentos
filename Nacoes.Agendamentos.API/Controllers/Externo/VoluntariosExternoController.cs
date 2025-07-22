﻿using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.API.Controllers.Externo;

[Route("api/voluntarios-externo")]
public sealed class VoluntariosExternoController : NacoesController
{
    #region Adicionar
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromServices] ICommandHandler<AdicionarVoluntarioCommand, Id<Voluntario>> handler,
                                               [FromServices] IAmbienteContext ambienteContext,
                                               [FromBody] AdicionarVoluntarioCommand command)
    {
        ambienteContext.StartBotSession();
        var result = await handler.Handle(command);

        return result.AsHttpResult(mensagem: "Voluntario adicionado com sucesso.");
    }
    #endregion
}