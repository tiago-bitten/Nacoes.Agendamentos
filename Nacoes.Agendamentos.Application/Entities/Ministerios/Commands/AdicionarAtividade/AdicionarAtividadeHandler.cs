using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.DomainEvents;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;

internal sealed class AdicionarAtividadeHandler(
    INacoesDbContext context)
    : ICommandHandler<AdicionarAtividadeCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(AdicionarAtividadeCommand command, CancellationToken cancellation = default)
    {
        var ministerio = await context.Ministerios.SingleOrDefaultAsync(x => x.Id == command.MinisterioId, cancellation);
        if (ministerio is null)
        {
            return MinisterioErrors.NaoEncontrado;
        }
        
        var existeAtividadeComNome = await context.Atividades
            .AnyAsync(x => x.MinisterioId == command.MinisterioId && x.Nome == command.Nome, cancellation);
        if (existeAtividadeComNome)
        {
            return AtividadeErrors.NomeEmUso;
        }
        
        var atividadeResult = ministerio.AdicionarAtividade(command.Nome, command.Descricao);
        if (atividadeResult.IsFailure)
        {
            return atividadeResult.Error;
        }
        
        var atividade = atividadeResult.Value;
        
        ministerio.Raise(new AtividadeAdicionadaDomainEvent(atividade.Id));
        await context.SaveChangesAsync(cancellation);

        return Result<Guid>.Success(atividade.Id);
    }
}