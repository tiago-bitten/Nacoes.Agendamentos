using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Agendas.Mappings;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Adicionar;

internal sealed class AdicionarAgendaHandler(IUnitOfWork uow,
                                             IAgendaRepository agendaRepository)
    : ICommandHandler<AdicionarAgendaCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AdicionarAgendaCommand command, CancellationToken cancellation = default)
    {
        var agendaResult = command.ToEntity();
        if (agendaResult.IsFailure)
        {
            return agendaResult.Error;
        }
        
        var agenda = agendaResult.Value;

        await uow.BeginAsync();
        await agendaRepository.AddAsync(agenda);
        
        agenda.Raise(new AgendaAdicionadaDomainEvent(agenda.Id));
        await uow.CommitAsync(cancellation);

        return Result<Guid>.Success(agenda.Id);
    }
}
