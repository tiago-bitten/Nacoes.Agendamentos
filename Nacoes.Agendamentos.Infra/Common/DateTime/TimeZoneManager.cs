using Nacoes.Agendamentos.Application.Common.DateTime;
using TimeZoneConverter;

namespace Nacoes.Agendamentos.Infra.Common.DateTime;

internal sealed class TimeZoneManager : ITimeZoneManager
{
    public IEnumerable<string> Get() => TZConvert.KnownIanaTimeZoneNames;

    public DateTimeOffset ConvertUtcToClient(DateTimeOffset utc, string timeZoneId)
    {
        var tz = TZConvert.GetTimeZoneInfo(timeZoneId);
        return TimeZoneInfo.ConvertTime(utc, tz);
    }

    public System.DateTime ConvertClientToUtc(System.DateTime client, string timeZoneId)
    {
        var tz = TZConvert.GetTimeZoneInfo(timeZoneId);
        return TimeZoneInfo.ConvertTimeToUtc(client, tz);
    }
}