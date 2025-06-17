using AgendaId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agenda>;
using VoluntarioMinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.VoluntarioMinisterio>;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;

public interface IAgendamentoChecker
{
    Task<bool> VoluntarioAgendado(VoluntarioMinisterioId voluntarioMinisterioId, AgendaId agendaId);
}