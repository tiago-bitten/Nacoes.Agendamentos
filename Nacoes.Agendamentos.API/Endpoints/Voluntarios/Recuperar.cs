using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Pagination;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Queries.Recuperar;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.API.Endpoints.Voluntarios;

internal sealed class Recuperar : IEndpoint
{
    public sealed record Request(string? Nome) : BaseQueryParam;
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/voluntarios", async (
            [AsParameters] Request request,
            [FromServices] INacoesDbContext context,
            CancellationToken cancellationToken) =>
        {
            Result<PagedResponse<VoluntarioResponse>> result;
            
            try
            {
                var filtrarNome = !string.IsNullOrEmpty(request.Nome);
                
                var voluntarios = await context.Voluntarios
                    .WhereIf(filtrarNome, x => x.Nome.Contains(request.Nome!))
                    .Select(x => new VoluntarioResponse
                    {
                        Id = x.Id,
                        DataCriacao = x.DataCriacao,
                        Nome = x.Nome,
                        Ministerios = x.Ministerios.Select(m => new VoluntarioResponse.MinisterioItem
                        {
                            Nome = m.Ministerio.Nome
                        }).ToList()
                    }).ToPagedResponseAsync(request.Limit, request.Cursor, cancellationToken);

                result = Result<PagedResponse<VoluntarioResponse>>.Success(voluntarios);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("RecuperarVoluntarios", ex.Message);
                result = Result<PagedResponse<VoluntarioResponse>>.Failure(error);
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Voluntarios);
    }
}