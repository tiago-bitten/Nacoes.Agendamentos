namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.AdicionarVoluntario;

public record AdicionarVoluntarioCommand
{
    public required string Nome { get; init; }
    public string? Email { get; init; }
    public CelularItem? Celular { get; init; }
    public string? Cpf { get; init; }
    public DateOnly? DataNascimento { get; init; }

    public record CelularItem
    {
        public required string Ddd { get; init; }
        public required string Numero { get; init; }
    }
}
