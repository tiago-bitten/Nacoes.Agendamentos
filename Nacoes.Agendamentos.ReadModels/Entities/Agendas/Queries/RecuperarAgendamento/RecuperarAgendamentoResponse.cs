using Nacoes.Agendamentos.Domain.Entities.Agendas;

namespace Nacoes.Agendamentos.ReadModels.Entities.Agendas.Queries.RecuperarAgendamento;

public record RecuperarAgendamentoResponse
{
    public List<Item> Items { get; init; } = [];

    public record Item
    {
        public Guid Id { get; init; }
        public required string VoluntarioNome { get; init; }
        public required string MinisterioNome { get; init; }
        public required string AtividadeNome { get; init; }
        public EOrigemAgendamento Origem { get; init; }
    }
}
