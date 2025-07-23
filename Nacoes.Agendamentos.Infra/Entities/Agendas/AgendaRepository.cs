using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Agendas;

internal class AgendaRepository : BaseRepository<Agenda>, IAgendaRepository
{
    #region Constructors
    public AgendaRepository(NacoesDbContext dbContext)
        : base(dbContext)
    {
    }
    #endregion
}
