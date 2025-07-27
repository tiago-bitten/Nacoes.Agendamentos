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
    public Task AuditAsync(Guid entidadeId, string acao, EHistoricoTipoAcao tipoAcao, string? detalhes)
    {
        return RegistrarAsync(entidadeId, acao, tipoAcao, detalhes);
    }

    public Task AuditAsync<T>(T entidade, string acao, EHistoricoTipoAcao tipoAcao, string? detalhes) where T : EntityId
    {
        return RegistrarAsync(entidade.Id, acao, tipoAcao, detalhes);
    }

    private async Task RegistrarAsync(Guid? entidadeId, string acao, EHistoricoTipoAcao tipoAcao, string? detalhes)
    {
        if (uow.HasActiveTransaction)
        {
            var dataHoje = DateTimeOffset.UtcNow;
            var usuarioId = Guid.Empty;

            if (ambienteContext.IsUsuarioAuthenticated)
            {
                usuarioId = ambienteContext.UserId;
            }

            await historicoRepository.AddAsync(Historico.Criar(entidadeId, dataHoje, usuarioId, acao, tipoAcao, detalhes));
            return;
        }

        await uow.BeginAsync();
        await RegistrarAsync(entidadeId, acao, tipoAcao, detalhes);
        await uow.CommitAsync();
    }
}