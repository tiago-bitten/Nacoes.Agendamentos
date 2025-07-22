using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;

public interface IHistoricoRegister
{
    Task AcaoCriarAsync<T>(T entidade, string acao = "Adicionado.") where T : EntityId<T>;
    Task AcaoEditarAsync<T>(T entidade, string? detalhes, string acao = "Editado.") where T : EntityId<T>;
    Task AcaoRemoverAsync<T>(T entidade, string acao = "Removido.") where T : EntityId<T>;
    Task AcaoLoginAsync(string acao, string detalhes);
}