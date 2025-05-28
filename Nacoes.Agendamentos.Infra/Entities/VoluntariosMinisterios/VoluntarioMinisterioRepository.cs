using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Domain.Entities.VoluntariosMinisterios;
using Nacoes.Agendamentos.Domain.Entities.VoluntariosMinisterios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.VoluntariosMinisterios;

public class VoluntarioMinisterioRepository : BaseRepository<VoluntarioMinisterio>, IVoluntarioMinisterioRepository
{
    #region Constructors
    public VoluntarioMinisterioRepository(NacoesDbContext dbContext)
        : base(dbContext)
    {
    }
    #endregion
}
