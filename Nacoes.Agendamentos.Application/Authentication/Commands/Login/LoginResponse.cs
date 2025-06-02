namespace Nacoes.Agendamentos.Application.Authentication.Commands.Login;

public record class LoginResponse
{
    public string AuthToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiraEm { get; set; }
}
