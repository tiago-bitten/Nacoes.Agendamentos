using Domain.Shared.Entities;

namespace Domain.Historicos.Interfaces;

public interface IHistoricoRegister
{
    Task AuditAsync(Guid entidadeId, string acao, string? detalhes = null);
    Task AuditAsync<T>(T entidade, string acao, string? detalhes = null) where T : Entity;
}
