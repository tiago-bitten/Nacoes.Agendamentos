using System.Text.RegularExpressions;

namespace Domain.Shared.ValueObjects;

public sealed record CPF
{
    public string Number { get; }

    public CPF(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
        {
            throw new ArgumentException("CPF cannot be empty.", nameof(number));
        }

        var digitsOnly = Clean(number);

        if (!IsValid(digitsOnly))
        {
            throw new ArgumentException("Invalid CPF.", nameof(number));
        }

        Number = digitsOnly;
    }

    private static string Clean(string cpf) =>
        Regex.Replace(cpf, "[^0-9]", "");

    private static bool IsValid(string cpf)
    {
        if (cpf.Length != 11 || new string(cpf[0], 11) == cpf)
        {
            return false;
        }

        int[] multiplier1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplier2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        var tempCpf = cpf[..9];
        var sum = 0;

        for (var i = 0; i < 9; i++)
        {
            sum += (tempCpf[i] - '0') * multiplier1[i];
        }

        var remainder = sum % 11;
        var digit1 = remainder < 2 ? 0 : 11 - remainder;

        tempCpf += digit1;
        sum = 0;

        for (var i = 0; i < 10; i++)
        {
            sum += (tempCpf[i] - '0') * multiplier2[i];
        }

        remainder = sum % 11;
        var digit2 = remainder < 2 ? 0 : 11 - remainder;

        return cpf.EndsWith($"{digit1}{digit2}");
    }

    public override string ToString() => Convert.ToUInt64(Number).ToString(@"000\.000\.000\-00");

    public bool Equals(CPF? other) => Number == other?.Number;
    public override int GetHashCode() => Number.GetHashCode();

    public static implicit operator string(CPF cpf) => cpf.ToString();
}
