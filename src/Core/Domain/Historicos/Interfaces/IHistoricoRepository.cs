namespace Domain.Historicos.Interfaces;

public interface IHistoricoRepository
{
    Task AddAsync(Historico historico);
    Task<List<Historico>> RecuperarPorEntidadeIdAsync(Guid domainId);
}
