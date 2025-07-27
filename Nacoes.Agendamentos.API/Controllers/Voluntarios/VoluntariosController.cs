using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Desvincular;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Vincular;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Queries.Recuperar;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.API.Controllers.Voluntarios;

public sealed class VoluntariosController : NacoesAuthenticatedController
{
    #region Adicionar
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromServices] ICommandHandler<AdicionarVoluntarioCommand, Id<Voluntario>> handler,
                                               [FromBody] AdicionarVoluntarioCommand command)
    {
        var result = await handler.Handle(command);

        return result.AsHttpResult(mensagem: "Voluntário adicionado com sucesso.");
    }
    #endregion
    
    #region Recuperar

    [HttpGet]
    public async Task<IActionResult> Recuperar([FromServices] IQueryHandler<RecuperarVoluntariosQuery, List<VoluntarioResponse>> handler, 
                                               [FromQuery] RecuperarVoluntariosQuery query)
    {
        var result = await handler.Handle(query);

        return result.AsHttpResult(mensagem: "Voluntários recuperados com sucesso.");
    }

    #endregion

    #region VincularMinisterio
    [HttpPost("{voluntarioId:guid}/ministerios")]
    public async Task<IActionResult> VincularMinisterio([FromServices] ICommandHandler<VincularVoluntarioMinisterioCommand> handler,
                                                        [FromBody] VincularVoluntarioMinisterioCommand command,
                                                        [FromRoute] Guid voluntarioId)
    {
        command.VoluntarioId = voluntarioId;
        var result = await handler.Handle(command);

        return result.AsHttpResult(mensagem: "Voluntário vinculado ao ministério com sucesso.");
    }
    #endregion
    
    #region DesvincularMinisterio
    [HttpDelete("/ministerios/{voluntarioMinisterioId:guid}")]    
    public async Task<IActionResult> DesvincularMinisterio([FromServices] ICommandHandler<DesvincularVoluntarioMinisterioCommand> handler,
                                                           [FromRoute] Guid voluntarioMinisterioId)
    {
        var command = new DesvincularVoluntarioMinisterioCommand
        {
            VoluntarioMinisterioId = voluntarioMinisterioId
        };
        var result = await handler.Handle(command);

        return result.AsHttpResult(mensagem: "Voluntário desvinculado ao ministério com sucesso.");
    }
    #endregion
}
