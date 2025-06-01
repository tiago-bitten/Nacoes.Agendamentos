using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;

namespace Nacoes.Agendamentos.API.Controllers.Usuarios;

public class UsuariosController : NacoesAuthenticatedController
{
    #region Adicionar
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromServices] IAdicionarUsuarioHandler handler,
                                               [FromBody] AdicionarUsuarioCommand command)
    {
        var resposta = await handler.ExecutarAsync(command);

        return Responder(resposta.Montar());
    }
    #endregion
}
