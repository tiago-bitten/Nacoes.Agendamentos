using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;
using VoluntarioMinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.VoluntarioMinisterio>;

namespace Nacoes.Agendamentos.Infra.Entities.Voluntarios;

public class VoluntarioRepository : BaseRepository<Voluntario>, IVoluntarioRepository
{
    #region Constructors
    public VoluntarioRepository(NacoesDbContext dbContext)
        : base(dbContext)
    {
    }
    #endregion

    #region RecuperarPorVoluntarioMinisterio
    public IQueryable<Voluntario> RecuperarPorVoluntarioMinisterio(VoluntarioMinisterioId voluntarioMinisterioId)
    {
        return GetAll()
            .Where(v => v.Ministerios.Any(m => m.Id == voluntarioMinisterioId))
            .AsSplitQuery();
    }
    #endregion
    
    #region RecuperarPorEmailAddress
    public Task<Voluntario?> RecuperarPorEmailAddressAsync(string emailAddress)
    {
        return GetAll()
            .SingleOrDefaultAsync(x => x.Email == emailAddress);
    }

    #endregion
}
