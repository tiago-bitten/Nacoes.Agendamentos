using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.API.Endpoints.Usuarios;

internal sealed class RecuperarPorId : IEndpoint
{
    public sealed record Response
    {
        public Guid Id { get; init; }
        public string Nome { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public List<MinisterioItem> Ministerios { get; init; } = [];

        public sealed record MinisterioItem
        {
            public Guid Id { get; init; }
            public string Nome { get; init; } = string.Empty;
            public string Cor { get; init; } = string.Empty;
        }
    }
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/usuarios/{id:guid}", async (
            [FromServices] INacoesDbContext context,
            [FromRoute] Guid id,
            CancellationToken cancellationToken) =>
        {
            Result<Response> result;
            
            try
            {
                var usuario = await context.Usuarios
                    .AsNoTracking()
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        Nome = x.Nome,
                        Email = x.Email,
                        Ministerios = x.Ministerios.Select(m => new Response.MinisterioItem
                        {
                            Id = m.Id,
                            Nome = m.Ministerio.Nome,
                            Cor = m.Ministerio.Cor.ToCssString()
                        }).ToList()
                    }).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
                
                result = Result<Response>.Success(usuario);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("RecuperarUsuarioPorId", ex.Message);
                result = Result<Response>.Failure(error);
            }
            
            return result.Match(Results.Ok, CustomResults.Problem);
        });
    }
}