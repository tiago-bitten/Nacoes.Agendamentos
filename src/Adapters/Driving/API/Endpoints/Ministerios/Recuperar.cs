using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using API.Infra;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Shared.Pagination;
using Application.Extensions;
using Domain.Shared.Results;

namespace API.Endpoints.Ministerios;

internal sealed class Recuperar : IEndpoint
{
    public sealed record Request(string? Nome) : BaseQueryParam;

    public sealed record Response : ICursorResponse
    {
        public Guid Id { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public string Nome { get; init; } = string.Empty;
        public string? Descricao { get; init; } = string.Empty;
        public string Cor { get; init; } = string.Empty;
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/ministerios", async (
            [AsParameters] Request request,
            [FromServices] INacoesDbContext context,
            CancellationToken cancellationToken) =>
        {
            Result<PagedResponse<Response>> result;

            try
            {
                var filtrarNome = !string.IsNullOrEmpty(request.Nome);

                var ministerios = await context.Ministerios
                    .AsNoTracking()
                    .WhereIf(filtrarNome, x => x.Nome.Contains(request.Nome!))
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        CreatedAt = x.CreatedAt,
                        Nome = x.Nome,
                        Descricao = x.Descricao,
                        Cor = x.Cor.ToString()
                    }).ToPagedResponseAsync(request.Limit, request.Cursor, cancellationToken);

                result = Result<PagedResponse<Response>>.Success(ministerios);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("RecuperarMinisterios", ex.Message);
                result = Result<PagedResponse<Response>>.Failure(error);
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Ministerios);
    }
}
