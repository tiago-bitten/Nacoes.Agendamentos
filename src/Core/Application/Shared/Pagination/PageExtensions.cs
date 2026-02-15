using Microsoft.EntityFrameworkCore;

namespace Application.Shared.Pagination;

public static class PageExtensions
{
    public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(
        this IQueryable<T> query,
        int limit, string? cursor,
        CancellationToken ct = default)
        where T : ICursorResponse
    {
        var total = await query.CountAsync(ct);

        query = query.OrderByDescending(item => item.CreatedAt).ThenByDescending(item => item.Id);

        if (!string.IsNullOrEmpty(cursor))
        {
            var decoded = Cursor.Decode(cursor);
            if (decoded is null)
            {
                throw new ArgumentException($"The cursor {cursor} is not valid", nameof(cursor));
            }

            DateTimeOffset cursorDate = decoded.Value.Date;
            Guid cursorId = decoded.Value.LastId;

            query = query.Where(item => item.CreatedAt < cursorDate ||
                                        (item.CreatedAt == cursorDate && item.Id < cursorId));
        }

        var items = await query.Take(limit + 1).ToListAsync(ct);
        var hasNext = items.Count > limit;
        var nextCursor = string.Empty;

        if (hasNext)
        {
            var lastItemOfCurrentPage = items[limit - 1];
            nextCursor = Cursor.Encode(lastItemOfCurrentPage.CreatedAt, lastItemOfCurrentPage.Id);
            items.RemoveAt(limit);
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
