using Application.Shared.Messaging;

namespace Application.Reports.Queries.InfoDiariaUsoApp;

public sealed record GetDailyAppUsageInfoQuery : IQuery<GetDailyAppUsageInfoResponse>
{
    public bool SendByEmail { get; init; }
}
