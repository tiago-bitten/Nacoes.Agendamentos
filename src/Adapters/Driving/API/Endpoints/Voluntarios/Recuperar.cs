using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Shared.Pagination;
using Application.Entities.Voluntarios.Queries.Recuperar;
using Application.Extensions;
using Domain.Shared.Results;

namespace API.Endpoints.Voluntarios;

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
                        CreatedAt = x.CreatedAt,
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
