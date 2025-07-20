using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using AtividadeId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Atividade>;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;

public sealed class AdicionarAtividadeHandler(IUnitOfWork uow,
                                              IValidator<AdicionarAtividadeCommand> atividadeValidator,
                                              IMinisterioRepository ministerioRepository)
    : ICommandHandler<AdicionarAtividadeCommand, AtividadeId>
{
    public async Task<Result<AtividadeId>> Handle(AdicionarAtividadeCommand command, CancellationToken cancellation = default)
    {
        await atividadeValidator.CheckAsync(command, cancellation);

        var ministerio = await ministerioRepository.GetByIdAsync(command.MinisterioId);

        /*var nomeExistente = await GetSpecification(new AtividadeComNomeExistenteSpecification(command.Nome, ministerioId),
                                                   ministerioRepository);
        if (nomeExistente)
        {
            return AtividadeErrors.AtividadeComNomeExistente;
        }*/

        await uow.BeginAsync();
        ministerio.AdicionarAtividade(command.Nome, command.Descricao);
        await uow.CommitAsync(cancellation);

        var atividade = ministerio.Atividades.Last();
        return Result<AtividadeId>.Success(atividade.Id);
    }
}