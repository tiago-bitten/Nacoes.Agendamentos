using System.Text.RegularExpressions;

namespace Domain.Shared.ValueObjects;

public sealed record class PhoneNumber : IEquatable<PhoneNumber>
{
    public string AreaCode { get; }
    public string Number { get; }

    public PhoneNumber(string areaCode, string number)
    {
        areaCode = Normalize(areaCode);
        number = Normalize(number);

        if (!Regex.IsMatch(areaCode, @"^\d{2}$"))
        {
            throw new ArgumentException("Area code must contain 2 numeric digits.", nameof(areaCode));
        }

        if (!Regex.IsMatch(number, @"^\d{9}$"))
        {
            throw new ArgumentException("Phone number must contain 9 numeric digits.", nameof(number));
        }

        AreaCode = areaCode;
        Number = number;
    }

    private static string Normalize(string value) =>
        Regex.Replace(value, @"\D", "");

    public override string ToString() =>
        $"({AreaCode}) {Number[..5]}-{Number[5..]}";

    public bool Equals(PhoneNumber? other) =>
        other is not null && AreaCode == other.AreaCode && Number == other.Number;

    public override int GetHashCode() =>
        HashCode.Combine(AreaCode, Number);
}
