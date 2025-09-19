using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Application.Common.Filters;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.API.Controllers.Externo;

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