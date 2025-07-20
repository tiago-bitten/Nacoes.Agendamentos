using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Agendas.Mappings;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;
using AgendaId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agenda>;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgenda;

public class AdicionarAgendaHandler(IUnitOfWork uow,
                                    IValidator<AdicionarAgendaCommand> agendaValidator,
                                    IAgendaRepository agendaRepository)
    : ICommandHandler<AdicionarAgendaCommand, AgendaId>
{
    public async Task<Result<AgendaId>> Handle(AdicionarAgendaCommand command, CancellationToken cancellation = default)
    {
        var commandResult = await agendaValidator.CheckAsync(command, cancellation);
        if (commandResult.IsFailure)
        {
            return commandResult.Error;
        }
        
        var agendaResult = command.ToEntity();
        if (agendaResult.IsFailure)
        {
            return agendaResult.Error;
        }
        
        var agenda = agendaResult.Value;

        await uow.BeginAsync();
        await agendaRepository.AddAsync(agenda);
        await uow.CommitAsync(cancellation);

        return Result<AgendaId>.Success(agenda.Id);
    }
}
