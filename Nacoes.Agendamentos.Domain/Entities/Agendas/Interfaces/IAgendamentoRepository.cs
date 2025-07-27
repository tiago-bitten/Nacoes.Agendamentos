using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;

public interface IAgendamentoRepository : IBaseRepository<Agendamento>
{
    IQueryable<Agendamento> RecuperarPorVoluntarioMinisterioAgenda(Guid voluntarioMinisterioId,
                                                                   Guid agendaId);
}