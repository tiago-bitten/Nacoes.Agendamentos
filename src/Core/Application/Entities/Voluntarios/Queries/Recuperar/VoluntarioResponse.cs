using Application.Shared.Pagination;

namespace Application.Entities.Voluntarios.Queries.Recuperar;

public sealed record VolunteerResponse : ICursorResponse
{
    public required Guid Id { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required string Name { get; init; }
    public List<MinistryItem> Ministries { get; init; } = [];

    public sealed record MinistryItem
    {
        public required string Name { get; init; }
    }
}
