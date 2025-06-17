using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarMinisterio;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarUsuario;
using Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.ValueObjects;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.API.Controllers;

public class InternoController : NacoesController
{
    [HttpPost("montar-ambiente")]
    public async Task<IActionResult> MontarAmbiente([FromServices] IAdicionarMinisterioHandler ministerioHandler,
                                                    [FromServices] IAdicionarUsuarioHandler usuarioHandler,
                                                    [FromServices] NacoesDbContext context)
    {
        var existeAlgumaCoisa = await context.Usuarios.AsNoTracking().AnyAsync() ||
                                await context.Ministerios.AsNoTracking().AnyAsync();
        if (existeAlgumaCoisa)
        {
            return Responder(new ApiResponse<object>
            {
                Sucesso = false,
                Erro = new Error("Interno.MontarAmbiente",
                                 "O ambiente já está montado. Use e-mail nacoes@nacoes.com e senha 123456 para acessar."),
            });
        }
        
        var ministerioCommand = new AdicionarMinisterioCommand
        {
            Nome = "Estacionamento",
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
            Nome = "Nacoes Admin",
            Email = "nacoes@nacoes.com",
            AuthType = EAuthType.Local,
            Senha = "123456",
            Celular = new AdicionarUsuarioCommand.CelularItem
            {
                Ddd = "48",
                Numero = "999999999"
            },
            Ministerios = [ new AdicionarUsuarioCommand.MinisterioItem { Id = ministerio.Value!.Value.ToString() } ]
        };

        var usuario = await usuarioHandler.ExecutarAsync(usuarioCommand);

        var aprovarUsuarioSolicitacaoSql = "update usuario_aprovacao set status = 'Aprovado'";
        var aprovarUsuarioSolicitacaoMinisterioSql = "update usuario_aprovacao_ministerio set aprovado = true";
        
        await context.Database.ExecuteSqlRawAsync(aprovarUsuarioSolicitacaoSql);
        await context.Database.ExecuteSqlRawAsync(aprovarUsuarioSolicitacaoMinisterioSql);
        await context.SaveChangesAsync();
        
        await context.SaveChangesAsync();
        
        return Responder(usuario.Montar());
    }
}
