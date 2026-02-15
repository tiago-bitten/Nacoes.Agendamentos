using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Domain.Shared.Results;

namespace Application.Reports.Queries.InfoDiariaUsoApp;

internal sealed class GetDailyAppUsageInfoQueryHandler(INacoesDbContext context)
    : IQueryHandler<GetDailyAppUsageInfoQuery, GetDailyAppUsageInfoResponse>
{
    public async Task<Result<GetDailyAppUsageInfoResponse>> Handle(GetDailyAppUsageInfoQuery query,
        CancellationToken ct = default)
    {
        var today = DateTimeOffset.UtcNow.AddHours(-3);

        var volunteerRegistrationOriginsQuery = context.Volunteers
            .Where(x => x.CreatedAt.Date == today.Date)
            .GroupBy(x => x.RegistrationOrigin)
            .Select(x => new GetDailyAppUsageInfoResponse.VolunteerInfo.VolunteerOriginInfo
            {
                Origin = x.Key,
                Count = x.Count()
            });

        var response = new GetDailyAppUsageInfoResponse
        {
            Date = today,
            Volunteers = new GetDailyAppUsageInfoResponse.VolunteerInfo
            {
                Origins = await volunteerRegistrationOriginsQuery.ToListAsync(ct),
                TotalCount = await volunteerRegistrationOriginsQuery.SumAsync(x => x.Count, ct)
            }
        };

        return response;
    }
}
