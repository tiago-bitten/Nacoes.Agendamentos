using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;

namespace Nacoes.Agendamentos.API.Controllers.Agendas;

public sealed class AgendamentosController : NacoesAuthenticatedController
{
    #region Agendar
    [HttpPost]
    public async Task<IActionResult> Agendr([FromServices] IAgendarHandler handler, 
                                            [FromBody] AgendarCommand command)
    {
        var resultado = await handler.ExecutarAsync(command);

        return Responder(resultado.Montar());
    }
    #endregion
}