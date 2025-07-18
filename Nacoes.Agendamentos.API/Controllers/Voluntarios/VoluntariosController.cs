using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.AdicionarVoluntario;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.VincularVoluntarioMinisterio;

namespace Nacoes.Agendamentos.API.Controllers.Voluntarios;

public sealed class VoluntariosController : NacoesController
{
    #region Adicionar
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromServices] IAdicionarVoluntarioHandler handler,
                                               [FromBody] AdicionarVoluntarioCommand command)
    {
        var resultado = await handler.ExecutarAsync(command);

        return Ok();
    }
    #endregion

    #region VincularMinisterio
    [HttpPost("{voluntarioId:guid}/ministerio/{ministerio:guid}")]
    public async Task<IActionResult> VincularMinisterio([FromServices] IVincularVoluntarioMinisterioHandler handler,
                                                        [FromRoute] Guid voluntarioId,
                                                        [FromRoute] Guid ministerioId)
    {
        var command = new VincularVoluntarioMinisterioCommand
        {
            VoluntarioId = voluntarioId,
            MinisterioId = ministerioId
        };

        var resultado = await handler.ExecutarAsync(command);

        return Ok();
    }
    #endregion
}
