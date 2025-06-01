namespace Nacoes.Agendamentos.Application.Authentication.PasswordVerifiers;

public static class PasswordVerifier
{
    public static bool Execute(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);

    public static string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);
}
