using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Agendas;

internal sealed class AgendamentoRepository(NacoesDbContext dbContext)
    : BaseRepository<Agendamento>(dbContext), IAgendamentoRepository
{
    #region RecuperarPorVoluntarioMinisterioAgenda
    public IQueryable<Agendamento> RecuperarPorVoluntarioMinisterioAgenda(Guid voluntarioMinisterioId,
                                                                          Guid agendaId)
    {
        return GetAll().Where(a => a.VoluntarioMinisterioId == voluntarioMinisterioId && a.AgendaId == agendaId);
    }
    #endregion
}