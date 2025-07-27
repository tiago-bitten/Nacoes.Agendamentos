using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Nacoes.Agendamentos.Application.Common.Pagination;

public sealed record PagedResponse<T>
{
    public int Total { get; init; } = 0;
    public List<T> Items { get; init; } = null!;
    public bool HasNext { get; init; }
    public string? Cursor { get; init; } = string.Empty;
    
    public static PagedResponse<T> Create(List<T> items, int total, bool hasNext, string? cursor) => new()
    {
        Items = items,
        Total = total,
        HasNext = hasNext,
        Cursor = cursor
    };
}

public static class PagedResponseExtensions
{
    public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(
        this IQueryable<T> query,
        int limit,
        string? cursor,
        Expression<Func<T, DateTimeOffset>> createdAtSelector,
        Expression<Func<T, Guid>> idSelector)
    {
        var total = await query.CountAsync();

        if (!string.IsNullOrEmpty(cursor))
        {
            var decoded = Cursor.Decode(cursor);
            if (decoded is null)
            {
                throw new ArgumentException($"O cursor {cursor} não é válido", nameof(cursor));
            }

            var parameter = createdAtSelector.Parameters[0];

            var createdAtProperty = createdAtSelector.Body;
            var idProperty = idSelector.Body;

            var dateValue = Expression.Constant(decoded.Value.Date, typeof(DateTimeOffset));
            var idValue = Expression.Constant(decoded.Value.LastId, typeof(Guid));

            var dateComparison = Expression.LessThan(createdAtProperty, dateValue);

            var dateEqual = Expression.Equal(createdAtProperty, dateValue);
            var idComparison = Expression.LessThan(idProperty, idValue);

            var combined = Expression.OrElse(
                dateComparison,
                Expression.AndAlso(dateEqual, idComparison)
            );

            var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
            query = query.Where(lambda);
        }


        query = query.OrderByDescending(createdAtSelector).ThenByDescending(idSelector);
        var items = await query.Take(limit + 1).ToListAsync();
        var hasNext = items.Count > limit;
        var nextCursor = string.Empty;
        
        if (hasNext)
        {
            var last = items.Last();
            nextCursor = Cursor.Encode(createdAtSelector.Compile()(last), idSelector.Compile()(last));

            items.RemoveAt(items.Count - 1);
        }


        return new PagedResponse<T>
        {
            Items = items,
            Total = total,
            HasNext = hasNext,
            Cursor = nextCursor
        };
    }
}