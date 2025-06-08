using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using AtividadeId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Atividade>;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
public interface IMinisterioRepository : IBaseRepository<Ministerio>
{
    IQueryable<Ministerio> RecuperarPorAtividade(AtividadeId atividadeId);
}
