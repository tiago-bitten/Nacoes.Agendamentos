using System.Text.RegularExpressions;

namespace Domain.Shared.ValueObjects;

public sealed record Email : IEquatable<Email>
{
    public string Address { get; } = null!;
    public bool IsConfirmed { get; }
    public string? ConfirmationCode { get; }
    public DateTimeOffset? ConfirmationCodeExpiration { get; }

    private Email() { }

    public Email(string address,
                 bool isConfirmed = false,
                 string? confirmationCode = null,
                 DateTimeOffset? confirmationCodeExpiration = null)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Email address cannot be empty.", nameof(address));
        }

        if (!IsValidEmail(address))
        {
            throw new ArgumentException("Invalid email address.", nameof(address));
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

    public bool Equals(Email? other) =>
        Address.Equals(other?.Address, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode() =>
        Address.ToLowerInvariant().GetHashCode();

    public override string ToString() => Address;

    public Email Confirm(string code)
    {
        if (code != ConfirmationCode)
        {
            throw new InvalidOperationException("Invalid confirmation code.");
        }

        if (ConfirmationCodeExpiration.HasValue && ConfirmationCodeExpiration.Value < DateTimeOffset.UtcNow)
        {
            throw new InvalidOperationException("Confirmation code has expired.");
        }

        return new Email(Address, true);
    }

    public static Email GenerateConfirmation(string address)
    {
        var code = Guid.NewGuid().ToString("N")[..6].ToUpper();
        var expiration = DateTimeOffset.UtcNow.AddHours(6);
        return new Email(address, false, code, expiration);
    }

    public static implicit operator Email(string value) => new(value);
    public static implicit operator string(Email value) => value.Address;
}
