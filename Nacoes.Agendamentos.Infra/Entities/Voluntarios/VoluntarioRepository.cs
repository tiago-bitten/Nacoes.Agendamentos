using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Dtos;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;
using VoluntarioMinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.VoluntarioMinisterio>;

namespace Nacoes.Agendamentos.Infra.Entities.Voluntarios;

public class VoluntarioRepository : BaseRepository<Voluntario>, IVoluntarioRepository, IVoluntarioApplicationRepository
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
    public IQueryable<Voluntario> RecuperarPorEmailAddress(string emailAddress)
    {
        return GetAll()
            .Where(x => x.Email != null && x.Email.Address == emailAddress);
    }

    #endregion

    #region RecuperarLogin
    public Task<LoginVoluntarioDto?> RecuperarLoginAsync(string cpf, DateOnly dataNascimento)
    {
        return GetAll()
            .Where(x => x.Cpf != null && x.Cpf.Numero == cpf 
                   && x.DataNascimento != null && x.DataNascimento.Valor == dataNascimento)
            .Select(x => new LoginVoluntarioDto(
                x.Id,
                x.Email != null ? x.Email.Address : string.Empty))
            .SingleOrDefaultAsync();
    }
    #endregion
}
