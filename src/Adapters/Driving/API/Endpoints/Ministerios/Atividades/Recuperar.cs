using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using API.Infra;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Shared.Pagination;
using Application.Extensions;
using Domain.Shared.Results;

namespace API.Endpoints.Ministerios.Atividades;

internal sealed class Recuperar : IEndpoint
{
    public sealed record Request(string? Nome) : BaseQueryParam;

    public sealed record Response : ICursorResponse
    {
        public Guid Id { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public string Nome { get; init; } = string.Empty;
        public string? Descricao { get; init; }
        public Ministerioitem Ministerio { get; init; } = null!;

        public sealed record Ministerioitem
        {
            public Guid Id { get; init; }
            public string Nome { get; init; } = string.Empty;
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/ministerios/atividades", async (
            [AsParameters] Request request,
            [FromServices] INacoesDbContext context,
            CancellationToken cancellationToken) =>
        {
            Result<PagedResponse<Response>> result;

            try
            {
                var filtrarNome = !string.IsNullOrEmpty(request.Nome);

                var atividades = await context.Atividades
                    .AsNoTracking()
                    .WhereIf(filtrarNome, x => x.Nome.Contains(request.Nome!))
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        CreatedAt = x.CreatedAt,
                        Nome = x.Nome,
                        Descricao = x.Descricao,
                        Ministerio = new Response.Ministerioitem
                        {
                            Id = x.Ministerio.Id,
                            Nome = x.Ministerio.Nome
                        }
                    }).ToPagedResponseAsync(request.Limit, request.Cursor, cancellationToken);

                result = Result<PagedResponse<Response>>.Success(atividades);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("RecuperarAtividades", ex.Message);
                result = Result<PagedResponse<Response>>.Failure(error);
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Atividades);
    }
}
