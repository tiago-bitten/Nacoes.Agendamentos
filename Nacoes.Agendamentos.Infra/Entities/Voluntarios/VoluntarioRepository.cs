using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Voluntarios;

public class VoluntarioRepository : BaseRepository<Voluntario>, IVoluntarioRepository
{
    #region Constructors
    public VoluntarioRepository(NacoesDbContext dbContext)
        : base(dbContext)
    {
    }
    #endregion
}
