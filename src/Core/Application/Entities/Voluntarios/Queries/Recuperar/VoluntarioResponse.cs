using Application.Shared.Pagination;

namespace Application.Entities.Voluntarios.Queries.Recuperar;

public record VoluntarioResponse : ICursorResponse
{
    public required Guid Id { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required string Nome { get; init; }
    public List<MinisterioItem> Ministerios { get; init; } = [];

    public record MinisterioItem
    {
        public required string Nome { get; init; }
    }
}
