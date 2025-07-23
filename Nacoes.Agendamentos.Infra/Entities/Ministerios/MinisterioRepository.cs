using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;
using AtividadeId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Atividade>;

namespace Nacoes.Agendamentos.Infra.Entities.Ministerios;

internal class MinisterioRepository(NacoesDbContext dbContext)
    : BaseRepository<Ministerio>(dbContext), IMinisterioRepository
{
    #region RecuperarPorAtividade
    public IQueryable<Ministerio> RecuperarPorAtividade(AtividadeId atividadeId)
    {
        return GetAll()
            .Where(m => m.Atividades.Any(a => a.Id == atividadeId));
    }
    #endregion
}
