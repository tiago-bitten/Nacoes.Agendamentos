using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;

public record AdicionarVoluntarioCommand : ICommand<Id<Voluntario>>
{
    public required string Nome { get; init; }
    public string? Email { get; init; }
    public CelularItem? Celular { get; init; }
    public string? Cpf { get; init; }
    public DateOnly? DataNascimento { get; init; }
    public EOrigemCadastroVoluntario OrigemCadastro { get; init; }

    public record CelularItem
    {
        public required string Ddd { get; init; }
        public required string Numero { get; init; }
    }
}
