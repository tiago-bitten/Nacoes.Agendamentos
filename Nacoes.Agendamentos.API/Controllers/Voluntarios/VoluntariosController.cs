using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Vincular;
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

        return result.AsHttpResult(mensagem: "Voluntario adicionado com sucesso.");
    }
    #endregion

    #region VincularMinisterio
    [HttpPost("{voluntarioId:guid}/ministerio/{ministerio:guid}")]
    public async Task<IActionResult> VincularMinisterio([FromServices] ICommandHandler<VincularVoluntarioMinisterioCommand> handler,
                                                        [FromRoute] Guid voluntarioId,
                                                        [FromRoute] Guid ministerioId)
    {
        var command = new VincularVoluntarioMinisterioCommand
        {
            VoluntarioId = voluntarioId,
            MinisterioId = ministerioId
        };

        var result = await handler.Handle(command);

        return Ok();
    }
    #endregion
}
