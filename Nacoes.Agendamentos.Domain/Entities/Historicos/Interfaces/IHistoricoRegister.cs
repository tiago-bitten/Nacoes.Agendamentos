using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;

public interface IHistoricoRegister
{
    Task AuditAsync(Guid entidadeId, string acao, string? detalhes = null);
    Task AuditAsync<T>(T entidade, string acao, string? detalhes = null) where T : EntityId;
}