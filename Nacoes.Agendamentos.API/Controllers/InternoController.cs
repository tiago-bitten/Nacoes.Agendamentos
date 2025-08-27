using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.Adicionar;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.ValueObjects;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.API.Controllers;

public class InternoController : NacoesController
{
    [HttpPost("montar-ambiente")]
    public async Task<IActionResult> MontarAmbiente(
        INacoesDbContext context,
        IAmbienteContext ambienteContext,
        ICommandHandler<AdicionarMinisterioCommand, Guid> adicionarMinisterioHandler,
        ICommandHandler<AdicionarUsuarioCommand, Guid> adicionarUsuarioHandler)
    {
        ambienteContext.StartBotSession();
        
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
        
        var ministerioResult = await adicionarMinisterioHandler.Handle(new AdicionarMinisterioCommand
        {
            Nome = "Nacoes",
            Descricao = "Ministerio de Nacoes",
            Cor = new AdicionarMinisterioCommand.CorItem
            {
                Tipo = ETipoCor.Hex,
                Valor = "4d4d4d"
            },
        });
        
        if (ministerioResult.IsFailure)
        {
            return BadRequest(new ApiResponse<object>
            {
                Sucesso = false,
                Erro = ministerioResult.Error
            });
        }
        
        var ministerioId = ministerioResult.Value;
        
        var usuarioResult = await adicionarUsuarioHandler.Handle(new AdicionarUsuarioCommand
        {
            Nome = "Nacoes",
            Email = "nacoes@nacoes.com",
            Senha = "123456",
            MinisteriosIds = [ministerioId],
            Celular = new AdicionarUsuarioCommand.CelularItem
            {
                Ddd = "11",
                Numero = "999999999"
            },
            AuthType = EAuthType.Local
        });

        if (usuarioResult.IsFailure)
        {
            return BadRequest(new ApiResponse<object>
            {
                Sucesso = false,
                Erro = usuarioResult.Error
            });
        }
        
        return Ok(new
        {
            Email = "nacoes@nacoes.com",
            Senha = "123456"
        });
    }
}