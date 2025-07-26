using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using AtividadeId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Atividade>;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;

internal sealed class AdicionarAtividadeHandler(IUnitOfWork uow,
                                                IMinisterioRepository ministerioRepository)
    : ICommandHandler<AdicionarAtividadeCommand, AtividadeId>
{
    public async Task<Result<AtividadeId>> Handle(AdicionarAtividadeCommand command, CancellationToken cancellation = default)
    {
        var ministerio = await ministerioRepository.GetByIdAsync(command.MinisterioId);
        if (ministerio is null)
        {
            return MinisterioErrors.NaoEncontrado;
        }
        
        var existeAtividadeComNome = await ministerioRepository.RecuperarPorNomeAtividade(command.Nome)
                                                               .AnyAsync(cancellation);
        if (existeAtividadeComNome)
        {
            return AtividadeErrors.NomeEmUso;
        }
        
        await uow.BeginAsync();
        var atividadeResult = ministerio.AdicionarAtividade(command.Nome, command.Descricao);
        if (atividadeResult.IsFailure)
        {
            return atividadeResult.Error;
        }
        
        var atividade = atividadeResult.Value;
        
        ministerio.Raise(new AtividadeAdicionadaDomainEvent(atividade.Id));
        await uow.CommitAsync(cancellation);

        return Result<AtividadeId>.Success(atividade.Id);
    }
}