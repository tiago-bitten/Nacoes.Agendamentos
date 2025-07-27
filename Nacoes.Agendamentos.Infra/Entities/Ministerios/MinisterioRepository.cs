using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Ministerios;

internal class MinisterioRepository(NacoesDbContext dbContext)
    : BaseRepository<Ministerio>(dbContext), IMinisterioRepository
{
    #region RecuperarPorAtividade
    public IQueryable<Ministerio> RecuperarPorAtividade(Guid atividadeId)
    {
        return GetAll()
            .Where(m => m.Atividades.Any(a => a.Id == atividadeId));
    }
    #endregion
    
    #region RecuperarPorNomeAtividade
    public IQueryable<Ministerio> RecuperarPorNomeAtividade(string nomeAtividade)
    {
        return GetAll(includes: "Atividades")
            .Where(m => m.Atividades.Any(a => a.Nome == nomeAtividade))
            .AsSplitQuery();
    }
    #endregion
}
