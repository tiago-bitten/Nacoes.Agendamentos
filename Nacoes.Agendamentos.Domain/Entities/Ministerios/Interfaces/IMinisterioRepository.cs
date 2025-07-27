using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
public interface IMinisterioRepository : IBaseRepository<Ministerio>
{
    IQueryable<Ministerio> RecuperarPorAtividade(Guid atividadeId);
    IQueryable<Ministerio> RecuperarPorNomeAtividade(string nomeAtividade);
}
