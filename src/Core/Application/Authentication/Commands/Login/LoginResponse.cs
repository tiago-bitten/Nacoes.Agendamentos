namespace Application.Authentication.Commands.Login;

public record LoginResponse(string AuthToken, string RefreshToken);
