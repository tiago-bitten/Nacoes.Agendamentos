using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Dtos;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Voluntarios;

internal sealed class VoluntarioRepository(NacoesDbContext dbContext) : BaseRepository<Voluntario>(dbContext),
    IVoluntarioRepository, IVoluntarioApplicationRepository
{
    #region RecuperarPorVoluntarioMinisterio
    public IQueryable<Voluntario> RecuperarPorVoluntarioMinisterio(Guid voluntarioMinisterioId)
    {
        return GetAll(includes: "Ministerios.Ministerio")
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
