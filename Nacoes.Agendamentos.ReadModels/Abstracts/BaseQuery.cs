using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.ReadModels.Abstracts;
public abstract class BaseQuery(NacoesDbContext dbContext)
{
    protected NacoesDbContext DbContext => dbContext;
}
