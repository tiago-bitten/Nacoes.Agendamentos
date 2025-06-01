using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarMinisterio;
using Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.API.Controllers;

public class HealthController : NacoesController
{
    [HttpPost("Usuario")]
    public async Task<IActionResult> Adicionar([FromServices] IAdicionarMinisterioHandler ministerioHandler,
                                               [FromServices] IAdicionarUsuarioHandler usuarioHandler)
    {
        var ministerioCommand = new AdicionarMinisterioCommand
        {
            Nome = "Cabosse",
            Descricao = "Cuida das dependencias do estacionamento",
            Cor = new AdicionarMinisterioCommand.CorItem
            {
                Valor = "84JG76",
                Tipo = ETipoCor.Hex
            }
        };

        var ministerio = await ministerioHandler.ExecutarAsync(ministerioCommand);

        var usuarioCommand = new AdicionarUsuarioCommand
        {
            Nome = "Teste",
            Email = "5oK8t@example.com",
            AuthType = EAuthType.Local,
            Senha = "123456",
            Celular = new AdicionarUsuarioCommand.CelularItem
            {
                Ddd = "48",
                Numero = "999999999"
            },
            Ministerios = [ new AdicionarUsuarioCommand.MinisterioItem { Id = ministerio.Value.Value.ToString() } ]
        };

        var resposta = await usuarioHandler.ExecutarAsync(usuarioCommand);

        return Responder(resposta.Montar());
    }
}
