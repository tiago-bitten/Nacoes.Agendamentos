using Microsoft.AspNetCore.Mvc;
using API.Controllers.Abstracts;
using API.Extensions;
using Application.Shared.Messaging;
using Application.Authentication.Context;
using Application.Common.Filters;
using Application.Entities.Voluntarios.Commands.Adicionar;
using Domain.Voluntarios;
using Domain.Shared.ValueObjects;

namespace API.Controllers.Externo;

[VoluntarioAuthorization]
[Route("api/voluntarios-externo")]
public sealed class VoluntariosExternoController : NacoesController
{
    #region Adicionar
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromServices] ICommandHandler<AdicionarVoluntarioCommand, Guid> handler,
                                               [FromBody] AdicionarVoluntarioCommand command)
    {
        var result = await handler.HandleAsync(command);

        return result.AsHttpResult(mensagem: "Cadastro realizado com sucesso.");
    }
    #endregion
}
