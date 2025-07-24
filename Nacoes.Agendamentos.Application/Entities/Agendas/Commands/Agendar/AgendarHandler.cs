using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using AgendamentoId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agendamento>;

namespace Nacoes.agendamentos.application.entities.agendas.commands.agendar;

internal sealed class AgendarHandler(IUnitOfWork uow,
                                   IAgendaRepository agendaRepository,
                                   IVoluntarioMinisterioRepository voluntarioMinisterioRepository,
                                   IAtividadeRepository atividadeRepository)
    : ICommandHandler<AgendarCommand, AgendamentoId>
{
    public async Task<Result<AgendamentoId>> Handle(AgendarCommand command, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }
}