using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Agendas.Mappings;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Adicionar;

internal sealed class AdicionarAgendaHandler(INacoesDbContext context)
    : ICommandHandler<AdicionarAgendaCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(AdicionarAgendaCommand command, CancellationToken cancellation = default)
    {
        var agendaResult = command.ToEntity();
        if (agendaResult.IsFailure)
        {
            return agendaResult.Error;
        }
        
        var agenda = agendaResult.Value;

        await context.Agendas.AddAsync(agenda, cancellation);
        
        agenda.Raise(new AgendaAdicionadaDomainEvent(agenda.Id));

        await context.SaveChangesAsync(cancellation);

        return agenda.Id;
    }
}
