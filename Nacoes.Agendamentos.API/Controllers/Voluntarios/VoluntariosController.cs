using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.AdicionarVoluntario;

namespace Nacoes.Agendamentos.API.Controllers.Voluntarios;

public sealed class VoluntariosController : NacoesAuthenticatedController
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
}
