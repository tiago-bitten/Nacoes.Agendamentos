using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Application.Entities.Agendas.Mappings;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgenda;

public class AdicionarAgendaHandler(IUnitOfWork uow,
                                    IValidator<AdicionarAgendaCommand> agendaValidator,
                                    IAgendaRepository agendaRepository)
    : BaseHandler(uow), IAdicionarAgendaHandler
{
    public async Task<Result<Id<Agenda>, Error>> ExecutarAsync(AdicionarAgendaCommand command, CancellationToken cancellation = default)
    {
        await agendaValidator.CheckAsync(command);

        var agenda = command.GetEntidade();

        await Uow.BeginAsync();
        await agendaRepository.AddAsync(agenda);
        await Uow.CommitAsync(cancellation);

        return agenda.Id;
    }
}
