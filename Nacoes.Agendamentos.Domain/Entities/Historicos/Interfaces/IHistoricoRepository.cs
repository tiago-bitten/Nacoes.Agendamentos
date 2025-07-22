namespace Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;

public interface IHistoricoRepository
{
    Task AddAsync(Historico historico);
    // TODO: Aqui vai ser list mesmo ao invés de queryable
    Task<List<Historico>> RecuperarPorEntidadeIdAsync(string domainId);
}