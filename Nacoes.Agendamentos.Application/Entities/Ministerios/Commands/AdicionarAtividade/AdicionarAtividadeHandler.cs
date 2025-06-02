using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Errors;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Specifications;
using Nacoes.Agendamentos.Domain.Exceptions;
using Nacoes.Agendamentos.Domain.ValueObjects;
using Nacoes.Agendamentos.Infra.Extensions;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;

public sealed class AdicionarAtividadeHandler(IUnitOfWork uow,
                                              IValidator<AdicionarAtividadeCommand> atividadeValidator,
                                              IMinisterioRepository ministerioRepository)
    : BaseHandler(uow), IAdicionarAtivdadeHandler
{
    public async Task<Result<Id<Atividade>, Error>> ExecutarAsync(AdicionarAtividadeCommand command, Guid ministerioId, CancellationToken cancellation = default)
    {
        await atividadeValidator.CheckAsync(command);

        var ministerio = await ministerioRepository.GetByIdAsync(ministerioId)
                                                   .OrElse(Throw.MinisterioNaoEncontrado);

        var nomeExistente = await GetSpecification(new AtividadeComNomeExistenteSpecification(command.Nome, ministerioId),
                                                   ministerioRepository);
        if (nomeExistente)
        {
            return AtividadeErrors.AtividadeComNomeExistente;
        }

        await Uow.BeginAsync();
        ministerio.AdicionarAtividade(command.Nome, command.Descricao);
        await Uow.CommitAsync(cancellation);

        var atividade = ministerio.Atividades.Last();
        return atividade.Id;
    }
}
