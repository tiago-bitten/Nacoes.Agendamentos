using System.Text.Json;

namespace Nacoes.Agendamentos.Application.Common.Pagination;

public static class Cursor
{
    public static string Encode(DateTimeOffset date, Guid id)
    {
        var payload = $"{date.ToUnixTimeMilliseconds()}_{id}";
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(payload));
    }

    public static (DateTimeOffset Date, Guid LastId)? Decode(string cursor)
    {
        try
        {
            var decoded = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(cursor));
            var parts = decoded.Split('_');

            var timestamp = long.Parse(parts[0]);
            var id = Guid.Parse(parts[1]);
            return (DateTimeOffset.FromUnixTimeMilliseconds(timestamp), id);
        }
        catch
        {
            return null;
        }
    }
}