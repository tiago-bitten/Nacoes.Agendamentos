using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;

namespace Nacoes.Agendamentos.Infra.Entities.Historicos;

public sealed class HistoricoRegister(IHistoricoRepository historicoRepository,
                                      IAmbienteContext ambienteContext,
                                      IUnitOfWork uow) 
    : IHistoricoRegister
{
    public Task AcaoCriarAsync<T>(T entidade, string acao = "Adicionado.") where T : EntityId<T>
    {
        return RegistrarAsync(entidade.Id, acao, EHistoricoTipoAcao.Criar, null);
    }

    public Task AcaoEditarAsync<T>(T entidade, string? detalhes, string acao = "Atualizado.") where T : EntityId<T>
    {
        return RegistrarAsync(entidade.Id, acao, EHistoricoTipoAcao.Atualizar, detalhes);
    }

    public Task AcaoRemoverAsync<T>(T entidade, string acao = "Removido.") where T : EntityId<T>
    {
        return RegistrarAsync(entidade.Id, acao, EHistoricoTipoAcao.Remover, null);
    }

    public Task AcaoLoginAsync(string acao, string detalhes)
    {
        return RegistrarAsync(null, acao, EHistoricoTipoAcao.Login, detalhes);
    }

    private Task RegistrarAsync(string? entidadeId, string acao, EHistoricoTipoAcao tipoAcao, string? detalhes) 
        => uow.CommitAsync(() =>
        {
            var dataHoje = DateTimeOffset.UtcNow;
            var usuarioId = string.Empty;

            if (ambienteContext.IsUsuarioAuthenticated)
            {
                usuarioId = ambienteContext.UserId.ToString();
            }

            return historicoRepository.AddAsync(Historico.Criar(entidadeId, dataHoje, usuarioId, acao, tipoAcao,
                detalhes));
        });
}