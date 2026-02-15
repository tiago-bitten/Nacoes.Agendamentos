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
    public sealed record Request(string? Name) : BaseQueryParam;

    public sealed record Response : ICursorResponse
    {
        public Guid Id { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; } = string.Empty;
        public string Color { get; init; } = string.Empty;
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("v1/ministries", async (
            [AsParameters] Request request,
            [FromServices] INacoesDbContext context,
            CancellationToken ct) =>
        {
            Result<PagedResponse<Response>> result;

            try
            {
                var filterByName = !string.IsNullOrEmpty(request.Name);

                var ministries = await context.Ministries
                    .AsNoTracking()
                    .WhereIf(filterByName, x => x.Name.Contains(request.Name!))
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        CreatedAt = x.CreatedAt,
                        Name = x.Name,
                        Description = x.Description,
                        Color = x.Color.ToString()
                    }).ToPagedResponseAsync(request.Limit, request.Cursor, ct);

                result = Result<PagedResponse<Response>>.Success(ministries);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("GetMinistries", ex.Message);
                result = Result<PagedResponse<Response>>.Failure(error);
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Ministries);
    }
}
