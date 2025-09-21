using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.API.Endpoints.Ministerios;

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
        app.MapGet("api/v1/ministerios/lookup", async (
            [FromServices] INacoesDbContext context,
            [AsParameters] Request request,
            CancellationToken cancellationToken) =>
        {
            Result<List<Response>> result;
            
            try
            {
                var filtrarNome = !string.IsNullOrEmpty(request.Nome);
                
                var ministerios = await context.Ministerios
                    .AsNoTracking()
                    .Take(20)
                    .WhereIf(filtrarNome, x => x.Nome.Contains(request.Nome!))
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        Nome = x.Nome
                    }).ToListAsync(cancellationToken);

                result = Result<List<Response>>.Success(ministerios);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("LookupMinisterios", ex.Message);
                result = Result<List<Response>>.Failure(error);
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        });
    }
}

