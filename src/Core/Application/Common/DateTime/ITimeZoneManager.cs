namespace Application.Common.DateTime;

public interface ITimeZoneManager
{
    IEnumerable<string> Get();
    DateTimeOffset ConvertUtcToClient(DateTimeOffset utc, string timeZoneId);
    System.DateTime ConvertClientToUtc(System.DateTime usuarioDateTime, string timeZoneId);
}
