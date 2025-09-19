namespace Nacoes.Agendamentos.Application.Common.Pagination;

public sealed record PagedResponse<T>
{
    public int Total { get; init; }
    public List<T> Items { get; init; } = [];
    public bool HasNext { get; init; }
    public string? Cursor { get; init; }
}