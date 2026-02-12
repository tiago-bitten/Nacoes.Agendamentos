using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Controllers.Abstracts;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Authentication.Context;
using Application.Common.Dtos;
using Application.Common.Responses;
using Application.Entities.Ministerios.Commands.Adicionar;
using Application.Entities.Usuarios.Commands.Adicionar;
using Domain.Shared.Results;
using Domain.Usuarios;
using Domain.Shared.ValueObjects;
namespace API.Controllers;

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

        var ministerioResult = await adicionarMinisterioHandler.HandleAsync(new AdicionarMinisterioCommand(
            "Nacoes",
            "Mininsterio de Nacoes",
            new CorDto("4d4d4d", ETipoCor.Hex)));

        if (ministerioResult.IsFailure)
        {
            return BadRequest(new ApiResponse<object>
            {
                Sucesso = false,
                Erro = ministerioResult.Error
            });
        }

        var ministerioId = ministerioResult.Value;

        var usuarioResult = await adicionarUsuarioHandler.HandleAsync(new AdicionarUsuarioCommand
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
