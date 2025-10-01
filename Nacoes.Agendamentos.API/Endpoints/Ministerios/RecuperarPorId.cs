using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.API.Endpoints.Ministerios;

internal sealed class RecuperarPorId : IEndpoint
{
    public sealed record Response
    {
        public Guid Id { get; init; }
        public string Nome { get; init; } = string.Empty;
        public string? Descricao { get; init; } = string.Empty;
        public string Cor { get; init; } = string.Empty;
        public List<AtividadeItem> Atividades { get; init; } = [];
        
        public sealed record AtividadeItem
        {
            public Guid Id { get; init; }
            public string Nome { get; init; } = string.Empty;
        }
    }
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/ministerios/{id:guid}", async (
            [FromServices] INacoesDbContext context,
            [FromRoute] Guid id,
            CancellationToken cancellationToken) =>
        {
            Result<Response> result;
            
            try
            {
                var ministerio = await context.Ministerios
                    .AsNoTracking()
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        Nome = x.Nome,
                        Descricao = x.Descricao,
                        Cor = x.Cor.ToString(),
                        Atividades = x.Atividades.Select(a => new Response.AtividadeItem
                        {
                            Id = a.Id,
                            Nome = a.Nome
                        }).ToList()
                    }).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
                
                result = Result<Response>.Success(ministerio);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("RecuperarMinisterioPorId", ex.Message);
                result = Result<Response>.Failure(error);
            }
            
            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Ministerios);
    }
}