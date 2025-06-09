using Nacoes.Agendamentos.ReadModels.Abstracts;

namespace Nacoes.Agendamentos.ReadModels.Entities.Agendas.Queries.RecuperarAgendamento;
public record RecuperarAgendamentoParam : BaseQueryListParam
{
    public Guid AgendaId { get; set; }
}
