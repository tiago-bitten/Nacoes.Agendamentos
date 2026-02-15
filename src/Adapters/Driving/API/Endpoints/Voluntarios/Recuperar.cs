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
    public sealed record Request(string? Name) : BaseQueryParam;

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("v1/volunteers", async (
            [AsParameters] Request request,
            [FromServices] INacoesDbContext context,
            CancellationToken ct) =>
        {
            Result<PagedResponse<VolunteerResponse>> result;

            try
            {
                var filterByName = !string.IsNullOrEmpty(request.Name);

                var volunteers = await context.Volunteers
                    .WhereIf(filterByName, x => x.Name.Contains(request.Name!))
                    .Select(x => new VolunteerResponse
                    {
                        Id = x.Id,
                        CreatedAt = x.CreatedAt,
                        Name = x.Name,
                        Ministries = x.Ministries.Select(m => new VolunteerResponse.MinistryItem
                        {
                            Name = m.Ministry.Name
                        }).ToList()
                    }).ToPagedResponseAsync(request.Limit, request.Cursor, ct);

                result = Result<PagedResponse<VolunteerResponse>>.Success(volunteers);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("GetVolunteers", ex.Message);
                result = Result<PagedResponse<VolunteerResponse>>.Failure(error);
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Volunteers);
    }
}
