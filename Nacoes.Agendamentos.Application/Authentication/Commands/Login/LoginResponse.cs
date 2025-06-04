namespace Nacoes.Agendamentos.Application.Authentication.Commands.Login;

public record class LoginResponse
{
    public required string AuthToken { get; set; }
    public required string RefreshToken { get; set; }
}
