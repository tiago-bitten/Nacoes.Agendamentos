using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Historicos;

internal sealed class HistoricoRepository(NacoesDbContext context) : IHistoricoRepository
{
    private DbSet<Historico> Historicos => context.Set<Historico>(); 
    
    #region Add
    public async Task AddAsync(Historico historico)
    {
        await Historicos.AddAsync(historico);
    }
    #endregion

    #region RecuperarPorEntidadeId
    public Task<List<Historico>> RecuperarPorEntidadeIdAsync(Guid entidadeId)
    {
        return Historicos.Where(h => h.EntidadeId == entidadeId).ToListAsync();
    }
    #endregion
}