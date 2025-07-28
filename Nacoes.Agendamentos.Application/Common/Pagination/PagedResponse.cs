using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Nacoes.Agendamentos.Application.Common.Pagination;

public sealed record PagedResponse<T>
{
    public int Total { get; init; }
    public List<T> Items { get; init; } = null!;
    public bool HasNext { get; init; }
    public string? Cursor { get; init; }

    public static PagedResponse<T> Create(List<T> items, int total, bool hasNext, string? cursor) => new()
    {
        Items = items,
        Total = total,
        HasNext = hasNext,
        Cursor = cursor
    };
}