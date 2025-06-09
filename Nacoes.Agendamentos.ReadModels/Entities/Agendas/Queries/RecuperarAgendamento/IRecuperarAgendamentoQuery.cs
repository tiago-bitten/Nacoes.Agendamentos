using AgendaId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agenda>;

namespace Nacoes.Agendamentos.ReadModels.Entities.Agendas.Queries.RecuperarAgendamento;

public interface IRecuperarAgendamentoQuery
{
    Task<RecuperarAgendamentoResponse> ExecutarAsync(RecuperarAgendamentoParam param, AgendaId agendaId, CancellationToken cancellation = default);
}
