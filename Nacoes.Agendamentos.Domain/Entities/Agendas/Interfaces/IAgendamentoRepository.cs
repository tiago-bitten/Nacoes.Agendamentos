using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using AgendaId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agenda>;
using VoluntarioMinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.VoluntarioMinisterio>;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;

public interface IAgendamentoRepository : IBaseRepository<Agendamento>
{
    IQueryable<Agendamento> RecuperarPorVoluntarioMinisterioAgenda(VoluntarioMinisterioId voluntarioMinisterioId,
                                                                   AgendaId agendaId);
}