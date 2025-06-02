using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.AdicionarVoluntario;

public record AdicionarVoluntarioCommand
{
    public string Nome { get; init; }
    public string? Email { get; init; }
    public CelularItem? Celular { get; init; }
    public string? Cpf { get; init; }
    public DateOnly? DataNascimento { get; init; }

    public record CelularItem
    {
        public string Ddd { get; init; }
        public string Numero { get; init; }
    }
}
