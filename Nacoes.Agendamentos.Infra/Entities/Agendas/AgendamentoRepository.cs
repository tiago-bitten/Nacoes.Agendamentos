using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;
using VoluntarioMinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.VoluntarioMinisterio>;
using AgendaId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agenda>;

namespace Nacoes.Agendamentos.Infra.Entities.Agendas;

public sealed class AgendamentoRepository(NacoesDbContext dbContext)
    : BaseRepository<Agendamento>(dbContext), IAgendamentoRepository
{
    #region RecuperarPorVoluntarioMinisterioAgenda
    public IQueryable<Agendamento> RecuperarPorVoluntarioMinisterioAgenda(VoluntarioMinisterioId voluntarioMinisterioId,
                                                                          AgendaId agendaId)
    {
        return GetAll().Where(a => a.VoluntarioMinisterioId == voluntarioMinisterioId && a.AgendaId == agendaId);
    }
    #endregion
}