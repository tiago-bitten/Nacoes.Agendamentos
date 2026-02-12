using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Domain.Historicos;
using Domain.Historicos.Interfaces;

namespace Infrastructure.Repositories;

internal sealed class HistoricoRepository(INacoesDbContext context) : IHistoricoRepository
{
    private DbSet<Historico> Historicos => context.Historicos;

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
