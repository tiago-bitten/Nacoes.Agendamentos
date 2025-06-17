using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Ministerios;

public sealed class AtividadeRepository : BaseRepository<Atividade>, IAtividadeRepository
{
    #region Construtores
    public AtividadeRepository(NacoesDbContext dbContext) : base(dbContext)
    {
    }
    #endregion
}