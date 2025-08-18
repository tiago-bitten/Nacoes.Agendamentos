using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;

namespace Nacoes.Agendamentos.Infra.Entities.Historicos;

public sealed class HistoricoRegister(IHistoricoRepository historicoRepository,
                                      IAmbienteContext ambienteContext,
                                      INacoesDbContext context) 
    : IHistoricoRegister
{
    public Task AuditAsync(Guid entidadeId, string acao, EHistoricoTipoAcao tipoAcao, string? detalhes)
    {
        return RegistrarAsync(entidadeId, acao, tipoAcao, detalhes);
    }

    public Task AuditAsync<T>(T entidade, string acao, EHistoricoTipoAcao tipoAcao, string? detalhes) where T : EntityId
    {
        return RegistrarAsync(entidade.Id, acao, tipoAcao, detalhes);
    }

    private async Task RegistrarHistoricoInternoAsync(Guid? entidadeId, string acao, EHistoricoTipoAcao tipoAcao, EHistoricoUsuarioAcao usuarioAcao, string? detalhes, Guid usuarioId)
    {
        var historico = Historico.Criar(entidadeId, DateTimeOffset.UtcNow, usuarioId, acao, tipoAcao, usuarioAcao, detalhes);
        await historicoRepository.AddAsync(historico);
    }

    private async Task RegistrarAsync(Guid? entidadeId, string acao, EHistoricoTipoAcao tipoAcao, string? detalhes)
    {
        if (!ambienteContext.IsUsuarioAuthenticated)
        {
            throw new Exception("Nao foi possivel identificar o usuario.");
        }
        
        var usuarioAcao = ambienteContext.IsUsuario ? EHistoricoUsuarioAcao.Usuario : ambienteContext.IsBot ? 
            EHistoricoUsuarioAcao.Bot : EHistoricoUsuarioAcao.ThirdPartyUser;
        
        var usuarioId = ambienteContext.UserId;

        await RegistrarHistoricoInternoAsync(entidadeId, acao, tipoAcao, usuarioAcao, detalhes, usuarioId);
        await context.SaveChangesAsync();
    }
}