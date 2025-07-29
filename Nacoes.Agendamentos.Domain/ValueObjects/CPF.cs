using System.Text.RegularExpressions;

namespace Nacoes.Agendamentos.Domain.ValueObjects;

// ReSharper disable once InconsistentNaming
public sealed record CPF
{
    public string Numero { get; }

    public CPF(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
        {
            throw new ArgumentException("CPF não pode ser vazio.", nameof(numero));
        }

        var somenteNumeros = Limpar(numero);

        if (!IsValid(somenteNumeros))
        {
            throw new ArgumentException("CPF inválido.", nameof(numero));
        }

        Numero = somenteNumeros;
    }

    private static string Limpar(string cpf) =>
        Regex.Replace(cpf, "[^0-9]", "");

    private static bool IsValid(string cpf)
    {
        if (cpf.Length != 11 || new string(cpf[0], 11) == cpf)
            return false;

        int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        var tempCpf = cpf[..9];
        var soma = 0;

        for (var i = 0; i < 9; i++)
        {
            soma += (tempCpf[i] - '0') * multiplicador1[i];
        }

        var resto = soma % 11;
        var digito1 = resto < 2 ? 0 : 11 - resto;

        tempCpf += digito1;
        soma = 0;

        for (var i = 0; i < 10; i++)
        {
            soma += (tempCpf[i] - '0') * multiplicador2[i];
        }

        resto = soma % 11;
        var digito2 = resto < 2 ? 0 : 11 - resto;

        return cpf.EndsWith($"{digito1}{digito2}");
    }

    public override string ToString() => Convert.ToUInt64(Numero).ToString(@"000\.000\.000\-00");

    public bool Equals(CPF? other) => Numero == other?.Numero;
    public override int GetHashCode() => Numero.GetHashCode();
    
    public static implicit operator string(CPF cpf) => cpf.ToString();
}
