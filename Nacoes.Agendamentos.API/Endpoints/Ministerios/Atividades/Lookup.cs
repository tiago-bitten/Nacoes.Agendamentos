using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.API.Endpoints.Ministerios.Atividades;

internal sealed class Lookup : IEndpoint
{
    public sealed record Request(string? Nome);

    public sealed record Response
    {
        public Guid Id { get; init; }
        public string Nome { get; init; } = string.Empty;
    }
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/ministerios/atividades/lookup", async (
            [AsParameters] Request request,
            [FromServices] INacoesDbContext context,
            CancellationToken cancellationToken) =>
        {
            Result<List<Response>> result;
            
            try
            {
                var filtrarNome = !string.IsNullOrEmpty(request.Nome);
                
                var atividades = await context.Atividades
                    .AsNoTracking()
                    .Take(20)
                    .WhereIf(filtrarNome, x => x.Nome.Contains(request.Nome!))
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        Nome = x.Nome
                    }).ToListAsync(cancellationToken);
                
                result = Result<List<Response>>.Success(atividades);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("LookupAtividades", ex.Message);
                result = Result<List<Response>>.Failure(error);
            }
            
            return result.Match(Results.Ok, CustomResults.Problem);
        });
    }
}