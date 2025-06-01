using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Errors;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Mappings;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Validators;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Specifications;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarMinisterio;

public sealed class AdicionarMinisterioHandler(IUnitOfWork uow,
                                               IValidator<AdicionarMinisterioCommand> ministerioValidator,
                                               IMinisterioRepository ministerioRepository)
    : BaseHandler(uow), IAdicionarMinisterioHandler
{
    public async Task<Result<Id<Ministerio>, Error>> ExecutarAsync(AdicionarMinisterioCommand command)
    {
        await ministerioValidator.CheckAsync(command);

        var ministerio = command.GetEntidade();

        var nomeExistente = await GetSpecification(new MinisterioComNomeExistenteSpecification(ministerio.Nome),
                                                   ministerioRepository);
        if (nomeExistente)
        {
            return MinisterioErrors.MinisterioComNomeExistente;
        }

        await Uow.BeginAsync();
        await ministerioRepository.AddAsync(ministerio);
        await Uow.CommitAsync();

        return ministerio.Id;
    }
}
