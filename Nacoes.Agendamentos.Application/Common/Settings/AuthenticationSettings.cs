namespace Nacoes.Agendamentos.Application.Common.Settings;
public sealed class AuthenticationSettings
{
    public required JwtSettings Jwt { get; set; }
    public required GoogleSettings Google { get; set; }
}
