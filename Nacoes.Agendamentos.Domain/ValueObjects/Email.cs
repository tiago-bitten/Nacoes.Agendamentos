using System.Text.RegularExpressions;

namespace Nacoes.Agendamentos.Domain.ValueObjects;

public sealed record class Email : IEquatable<Email>
{
    public string Address { get; }
    public bool IsConfirmed { get; }
    public string? ConfirmationCode { get; }
    public DateTimeOffset? ConfirmationCodeExpiration { get; }

    public Email(string address,
                 bool isConfirmed = false,
                 string? confirmationCode = null,
                 DateTimeOffset? confirmationCodeExpiration = null)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Endereço de email não pode ser vazio.", nameof(address));
        }

        if (!IsValidEmail(address))
        {
            throw new ArgumentException("Endereço de email inválido.", nameof(address));
        }

        Address = address;
        IsConfirmed = isConfirmed;
        ConfirmationCode = confirmationCode;
        ConfirmationCodeExpiration = confirmationCodeExpiration;
    }

    private static bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public bool Equals(Email other) =>
        Address.Equals(other.Address, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode() =>
        Address.ToLowerInvariant().GetHashCode();

    public override string ToString() => Address;

    public Email Confirm(string code)
    {
        if (code != ConfirmationCode)
        {
            throw new InvalidOperationException("Código de confirmação inválido.");
        }

        if (ConfirmationCodeExpiration.HasValue && ConfirmationCodeExpiration.Value < DateTimeOffset.UtcNow)
        {
            throw new InvalidOperationException("Código de confirmação expirado.");
        }

        return new Email(Address, true, null, null);
    }

    public static Email GenerateConfirmation(string address)
    {
        var code = Guid.NewGuid().ToString("N")[..6].ToUpper();
        var expiration = DateTimeOffset.UtcNow.AddHours(6);
        return new Email(address, false, code, expiration);
    }
}
