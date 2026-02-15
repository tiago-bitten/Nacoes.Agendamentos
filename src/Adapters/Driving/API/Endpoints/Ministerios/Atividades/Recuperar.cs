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
    public sealed record Request(string? Name) : BaseQueryParam;

    public sealed record Response : ICursorResponse
    {
        public Guid Id { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public MinistryItem Ministry { get; init; } = null!;

        public sealed record MinistryItem
        {
            public Guid Id { get; init; }
            public string Name { get; init; } = string.Empty;
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("v1/ministries/activities", async (
            [AsParameters] Request request,
            [FromServices] INacoesDbContext context,
            CancellationToken ct) =>
        {
            Result<PagedResponse<Response>> result;

            try
            {
                var filterByName = !string.IsNullOrEmpty(request.Name);

                var activities = await context.Activities
                    .AsNoTracking()
                    .WhereIf(filterByName, x => x.Name.Contains(request.Name!))
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        CreatedAt = x.CreatedAt,
                        Name = x.Name,
                        Description = x.Description,
                        Ministry = new Response.MinistryItem
                        {
                            Id = x.Ministry.Id,
                            Name = x.Ministry.Name
                        }
                    }).ToPagedResponseAsync(request.Limit, request.Cursor, ct);

                result = Result<PagedResponse<Response>>.Success(activities);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("GetActivities", ex.Message);
                result = Result<PagedResponse<Response>>.Failure(error);
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Activities);
    }
}
