using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using API.Infra;
using Application.Shared.Contexts;
using Application.Extensions;
using Domain.Shared.Results;

namespace API.Endpoints.Ministerios;

internal sealed class Lookup : IEndpoint
{
    public sealed record Request(string? Name);

    public sealed record Response
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("v1/ministries/lookup", async (
            [FromServices] INacoesDbContext context,
            [AsParameters] Request request,
            CancellationToken ct) =>
        {
            Result<List<Response>> result;

            try
            {
                var filterByName = !string.IsNullOrEmpty(request.Name);

                var ministries = await context.Ministries
                    .AsNoTracking()
                    .Take(20)
                    .WhereIf(filterByName, x => x.Name.Contains(request.Name!))
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToListAsync(ct);

                result = Result<List<Response>>.Success(ministries);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("LookupMinistries", ex.Message);
                result = Result<List<Response>>.Failure(error);
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Ministries);
    }
}
