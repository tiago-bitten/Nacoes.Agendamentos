using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using API.Infra;
using Application.Shared.Contexts;
using Domain.Shared.Results;

namespace API.Endpoints.Ministerios;

internal sealed class RecuperarPorId : IEndpoint
{
    public sealed record Response
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; } = string.Empty;
        public string Color { get; init; } = string.Empty;
        public List<ActivityItem> Activities { get; init; } = [];

        public sealed record ActivityItem
        {
            public Guid Id { get; init; }
            public string Name { get; init; } = string.Empty;
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("v1/ministries/{id:guid}", async (
            [FromServices] INacoesDbContext context,
            [FromRoute] Guid id,
            CancellationToken ct) =>
        {
            Result<Response> result;

            try
            {
                var ministry = await context.Ministries
                    .AsNoTracking()
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Color = x.Color.ToString(),
                        Activities = x.Activities.Select(a => new Response.ActivityItem
                        {
                            Id = a.Id,
                            Name = a.Name
                        }).ToList()
                    }).SingleOrDefaultAsync(x => x.Id == id, ct);

                result = Result<Response>.Success(ministry);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("GetMinistryById", ex.Message);
                result = Result<Response>.Failure(error);
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Ministries);
    }
}
