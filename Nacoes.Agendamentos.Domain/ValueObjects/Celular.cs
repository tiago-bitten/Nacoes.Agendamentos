using System.Text.RegularExpressions;

namespace Nacoes.Agendamentos.Domain.ValueObjects;

public sealed record class Celular : IEquatable<Celular>
{
    public string Ddd { get; }
    public string Numero { get; }

    public Celular(string ddd, string numero)
    {
        ddd = Normalizar(ddd);
        numero = Normalizar(numero);

        if (!Regex.IsMatch(ddd, @"^\d{2}$"))
        {
            throw new ArgumentException("DDD deve conter 2 dígitos numéricos.", nameof(ddd));
        }

        if (!Regex.IsMatch(numero, @"^\d{9}$"))
        {
            throw new ArgumentException("Número de celular deve conter 9 dígitos numéricos.", nameof(numero));
        }

        Ddd = ddd;
        Numero = numero;
    }

    private static string Normalizar(string valor) =>
        Regex.Replace(valor, @"\D", "");

    public override string ToString() =>
        $"({Ddd}) {Numero[..5]}-{Numero[5..]}";

    public bool Equals(Celular other) =>
        Ddd == other.Ddd && Numero == other.Numero;

    public override int GetHashCode() =>
        HashCode.Combine(Ddd, Numero);
}
