using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;

public interface IHistoricoRegister
{
    Task AuditAsync(Guid entidadeId, string acao, EHistoricoTipoAcao tipoAcao = EHistoricoTipoAcao.Outro, string? detalhes = null);
    Task AuditAsync<T>(T entidade, string acao, EHistoricoTipoAcao tipoAcao = EHistoricoTipoAcao.Outro, string? detalhes = null) where T : EntityId;
}