using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Voluntarios;

public sealed class VoluntarioMinisterioRepository : BaseRepository<VoluntarioMinisterio>, IVoluntarioMinisterioRepository
{
    #region Constutores
    public VoluntarioMinisterioRepository(NacoesDbContext dbContext) : base(dbContext)
    {
    }
    #endregion
}