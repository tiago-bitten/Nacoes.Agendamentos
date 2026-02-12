namespace Domain.Shared.ValueObjects;

public sealed record DataNascimento
{
    public DateOnly Valor { get; }
    public int Idade => CalcularIdade(Valor);
    public bool MenorDeIdade => Idade < 18;

    public DataNascimento(DateOnly valor)
    {
        if (valor == DateOnly.FromDateTime(DateTime.Today))
        {
            throw new ArgumentException("Data de nascimento não pode ser hoje.", nameof(valor));
        }

        if (valor > DateOnly.FromDateTime(DateTime.Today))
        {
            throw new ArgumentException("Data de nascimento não pode ser no futuro.", nameof(valor));
        }

        var idade = CalcularIdade(valor);
        if (idade > 140)
        {
            throw new ArgumentException("Idade não pode ser superior a 140 anos.", nameof(valor));
        }

        Valor = valor;
    }

    private static int CalcularIdade(DateOnly nascimento)
    {
        var hoje = DateOnly.FromDateTime(DateTime.Today);
        var idade = hoje.Year - nascimento.Year;
        if (nascimento > hoje.AddYears(-idade)) idade--;
        return idade;
    }

    public override string ToString() => Valor.ToString("dd/MM/yyyy");

    public bool Equals(DataNascimento? other) => other != null && Valor == other.Valor;
    public override int GetHashCode() => Valor.GetHashCode();
}
