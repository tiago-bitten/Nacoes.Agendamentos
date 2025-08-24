namespace Nacoes.Agendamentos.Application.Common.DateTime;

public static class DateTimeExtensions
{
    public static System.DateTime ToUtc(this System.DateTime client, string timeZoneId, ITimeZoneManager manager)
        => manager.ConvertClientToUtc(client, timeZoneId);

    public static DateTimeOffset ToClient(this DateTimeOffset utc, string timeZoneId, ITimeZoneManager manager)
        => manager.ConvertUtcToClient(utc, timeZoneId);
}