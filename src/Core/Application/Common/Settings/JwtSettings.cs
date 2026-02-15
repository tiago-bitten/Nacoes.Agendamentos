namespace Application.Common.Settings;

public sealed class JwtSettings
{
    public required string Secret { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public int DurationInMinutes { get; set; }
}
