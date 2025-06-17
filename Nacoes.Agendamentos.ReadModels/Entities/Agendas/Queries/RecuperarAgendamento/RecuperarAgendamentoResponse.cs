using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.ReadModels.Abstracts;

namespace Nacoes.Agendamentos.ReadModels.Entities.Agendas.Queries.RecuperarAgendamento;

public record RecuperarAgendamentoResponse : BaseQueryListResponse
{
    public List<Item> Items { get; set; } = [];

    public record Item
    {
        public Guid Id { get; init; }
        public DateTime DataCriacao { get; init; }
        public required string VoluntarioNome { get; init; }
        public required string MinisterioNome { get; init; }
        public required string AtividadeNome { get; init; }
        public EOrigemAgendamento Origem { get; init; }
    }
}
