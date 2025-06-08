using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.AdicionarVoluntario;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.VincularVoluntarioMinisterio;
using Nacoes.Agendamentos.ReadModels.Entities.Voluntarios.Queries.RecuperarVoluntario;

namespace Nacoes.Agendamentos.API.Controllers.Voluntarios;

public sealed class VoluntariosController : NacoesController
{
    #region Adicionar
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromServices] IAdicionarVoluntarioHandler handler,
                                               [FromBody] AdicionarVoluntarioCommand command)
    {
        var resultado = await handler.ExecutarAsync(command);

        return Responder(resultado.Montar());
    }
    #endregion

    #region Recuperar
    [HttpGet]
    public async Task<IActionResult> Recuperar([FromServices] IRecuperarVoluntarioQuery query,
                                               [FromQuery] RecuperarVoluntarioParam param)
    {
        var resultado = await query.ExecutarAsync(param);

        return Responder(resultado.Items.Montar()
                                        .ComTotal(resultado.Total)
                                        .DefinirProximaPagina(resultado));
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

        return Responder(resultado.Montar());
    }
    #endregion
}
