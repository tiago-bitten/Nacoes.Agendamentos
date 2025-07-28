using Microsoft.EntityFrameworkCore;

namespace Nacoes.Agendamentos.Application.Common.Pagination;

public static class PageExtensions
{
    public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(this IQueryable<T> query, int limit, string? cursor) 
        where T : ICursorResponse
    {
        var total = await query.CountAsync();

        query = query.OrderByDescending(item => item.DataCriacao).ThenByDescending(item => item.Id);

        if (!string.IsNullOrEmpty(cursor))
        {
            var decoded = Cursor.Decode(cursor);
            if (decoded is null)
            {
                throw new ArgumentException($"O cursor {cursor} não é válido", nameof(cursor));
            }

            DateTimeOffset cursorDate = decoded.Value.Date;
            Guid cursorId = decoded.Value.LastId;

            query = query.Where(item => item.DataCriacao < cursorDate ||
                                        (item.DataCriacao == cursorDate && item.Id < cursorId)); 
        }

        var items = await query.Take(limit + 1).ToListAsync();
        var hasNext = items.Count > limit;
        var nextCursor = string.Empty;
        
        if (hasNext)
        {
            var lastItemOfCurrentPage = items[limit - 1];
            nextCursor = Cursor.Encode(lastItemOfCurrentPage.DataCriacao, lastItemOfCurrentPage.Id);
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