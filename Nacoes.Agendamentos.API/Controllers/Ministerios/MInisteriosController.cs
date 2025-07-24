using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarMinisterio;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.API.Controllers.Ministerios;
public class MinisteriosController : NacoesAuthenticatedController
{
    #region Adicionar
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromServices] ICommandHandler<AdicionarMinisterioCommand, Id<Ministerio>> handler,
                                               [FromBody] AdicionarMinisterioCommand command)
    {
        var result = await handler.Handle(command);

        return result.AsHttpResult(mensagem: "Ministerio adicionado com sucesso.");
    }
    #endregion

    #region AdicionarAtividade
    [HttpPost("{ministerioId:guid}/atividades")]
    public async Task<IActionResult> AdicionarAtividade([FromServices] ICommandHandler<AdicionarAtividadeCommand, Id<Atividade>> handler,
                                                        [FromBody] AdicionarAtividadeCommand command,
                                                        [FromRoute] Guid ministerioId)
    {
        var result = await handler.Handle(command);

        return result.AsHttpResult(mensagem: "Atividade adicionada com sucesso.");
    }
    #endregion
}
