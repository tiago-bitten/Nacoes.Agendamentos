using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Adicionar;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.API.Controllers.Agendas;

public class AgendasController : NacoesController
{
    #region Adicionar
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromServices] ICommandHandler<AdicionarAgendaCommand, Id<Agenda>> handler,
                                               [FromBody] AdicionarAgendaCommand command)
    {
        var result = await handler.Handle(command);

        return result.AsHttpResult(mensagem: "Agenda adicionada com sucesso.");
    }
    #endregion

    #region Recuperar Agendamentos
    [HttpGet("{agendaId:guid}/agendamentos")]
    public Task<IActionResult> RecuperarAgendamentos()
    {
        throw new NotImplementedException();
    }
    #endregion
}
