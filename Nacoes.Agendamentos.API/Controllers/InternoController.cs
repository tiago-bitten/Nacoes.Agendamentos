using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.API.Controllers;

public class InternoController : NacoesController
{
    [HttpPost("montar-ambiente")]
    public async Task<IActionResult> MontarAmbiente([FromServices] INacoesDbContext context,
                                                    [FromServices] ICommandHandler<AdicionarUsuarioCommand, Guid> adicionarUsuarioHandler)
    {
        var existeAlgumaCoisa = await context.Usuarios.AsNoTracking().AnyAsync() ||
                                await context.Ministerios.AsNoTracking().AnyAsync();
        if (existeAlgumaCoisa)
        {
            return Ok(new ApiResponse<object>
            {
                Sucesso = false,
                Erro = Error.Failure("Interno.MontarAmbiente", "O ambiente já está montado. Use e-mail nacoes@nacoes.com e senha 123456 para acessar."),
            });
        }
        
        var result = await adicionarUsuarioHandler.Handle(new AdicionarUsuarioCommand
        {
            Nome = "Nacoes",
            Email = "nacoes@nacoes.com",
            Senha = "123456",
            Celular = new AdicionarUsuarioCommand.CelularItem
            {
                Ddd = "11",
                Numero = "999999999"
            },
            AuthType = EAuthType.Local
        });

        if (result.IsFailure)
        {
            return BadRequest();
        }
        
        return Ok(new
        {
            Email = "nacoes@nacoes.com",
            Senha = "123456"
        });
    }
}