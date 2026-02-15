using Domain.Voluntarios;

namespace Application.Reports.Queries.InfoDiariaUsoApp;

public record GetDailyAppUsageInfoResponse
{
    public required DateTimeOffset Date { get; init; }
    public required VolunteerInfo Volunteers { get; init; }

    public record VolunteerInfo
    {
        public required List<VolunteerOriginInfo> Origins { get; init; } = [];
        public required int TotalCount { get; init; }

        public record VolunteerOriginInfo
        {
            public required EVolunteerRegistrationOrigin Origin { get; init; }
            public required int Count { get; init; }
        }
    }
}
