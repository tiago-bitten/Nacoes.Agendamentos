using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Shared.Pagination;
using Domain.Shared.Results;

namespace Application.Entities.Voluntarios.Queries.Recuperar;

internal sealed class GetVolunteersQueryHandler(INacoesDbContext context)
    : IQueryHandler<GetVolunteersQuery, PagedResponse<VolunteerResponse>>
{
    public async Task<Result<PagedResponse<VolunteerResponse>>> Handle(
        GetVolunteersQuery query,
        CancellationToken ct)
    {
        var volunteers = await context.Volunteers
            .AsNoTracking()
            .Select(x => new VolunteerResponse
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                Name = x.Name,
                Ministries = x.Ministries.Select(m => new VolunteerResponse.MinistryItem
                {
                    Name = m.Ministry.Name
                }).ToList()
            }).ToPagedResponseAsync(query.Limit, query.Cursor, ct);

        return volunteers;
    }
}
