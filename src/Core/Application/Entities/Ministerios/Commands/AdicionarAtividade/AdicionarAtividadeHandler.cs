using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Domain.Shared.Results;
using Domain.Ministerios;
using Domain.Ministerios.DomainEvents;

namespace Application.Entities.Ministerios.Commands.AdicionarAtividade;

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
