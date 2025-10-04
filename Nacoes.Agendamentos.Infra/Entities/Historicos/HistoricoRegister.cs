using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;
using Nacoes.Agendamentos.Domain.Enums;

namespace Nacoes.Agendamentos.Infra.Entities.Historicos;

public sealed class HistoricoRegister(
    IAmbienteContext ambienteContext, 
    INacoesDbContext context) 
    : IHistoricoRegister
{
    public async Task AuditAsync(Guid entidadeId, string acao, string? detalhes)
    {
        if (!ambienteContext.IsUserAuthenticated)
        {
            throw new Exception("Usuário não identificado");
        }
        
        var usuarioId = ambienteContext.UserId;
        var usuarioAcao = ambienteContext.UserContextType;
        
        var historico = Historico.Criar(entidadeId, usuarioId, acao, usuarioAcao, detalhes);

        await context.Historicos.AddAsync(historico);
        await context.SaveChangesAsync();    
    }

    public Task AuditAsync<T>(T entidade, string acao, string? detalhes) where T : EntityId
    {
        return AuditAsync(entidade.Id, acao, detalhes);
    }
}