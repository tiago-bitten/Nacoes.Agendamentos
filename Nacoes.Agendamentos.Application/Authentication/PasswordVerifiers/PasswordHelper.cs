namespace Nacoes.Agendamentos.Application.Authentication.PasswordVerifiers;

public static class PasswordHelper
{
    public static bool Verify(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);

    public static string Hash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);
}
