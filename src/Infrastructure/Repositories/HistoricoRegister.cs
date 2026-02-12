using Application.Shared.Contexts;
using Application.Authentication.Context;
using Domain.Shared.Entities;
using Domain.Historicos;
using Domain.Historicos.Interfaces;
using Domain.Enums;

namespace Infrastructure.Repositories;

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

    public Task AuditAsync<T>(T entidade, string acao, string? detalhes) where T : Entity
    {
        return AuditAsync(entidade.Id, acao, detalhes);
    }
}
