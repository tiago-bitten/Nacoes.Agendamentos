namespace Nacoes.Agendamentos.Application.Authentication.Commands.Login;

public record LoginResponse
{
    public required string AuthToken { get; set; }
    public required string RefreshToken { get; set; }
}
