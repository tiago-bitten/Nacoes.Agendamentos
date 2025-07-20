using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.API.Controllers.Abstracts;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarMinisterio;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.ValueObjects;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.API.Controllers;

public class InternoController : NacoesController
{
    [HttpPost("montar-ambiente")]
    public async Task<IActionResult> MontarAmbiente([FromServices] IAdicionarMinisterioHandler ministerioHandler,
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

        var insertUsuario = """
                            insert into "usuario" ("Id", "Nome", "Email", "Celular", "Senha", "AuthType")
                            values (1, 'Nacoes', 'nacoes@nacoes.com', '123456789', '123456', 'Email');
                            """;

        await context.Database.ExecuteSqlRawAsync(insertUsuario);

        await context.SaveChangesAsync();
        
        return Responder(new ApiResponse<object>());
    }
}
