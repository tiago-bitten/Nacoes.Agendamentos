using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using AgendamentoId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agendamento>;

namespace Nacoes.agendamentos.application.entities.agendas.commands.agendar;

public sealed class AgendarHandler(IUnitOfWork uow,
                                   IValidator<AgendarCommand> agendarValidator,
                                   IAgendaRepository agendaRepository,
                                   IVoluntarioMinisterioRepository voluntarioMinisterioRepository,
                                   IAtividadeRepository atividadeRepository)
    : BaseHandler(uow), IAgendarHandler
{
    public async Task<Result<AgendamentoId>> ExecutarAsync(AgendarCommand command, CancellationToken cancellation = default)
    {
        await agendarValidator.CheckAsync(command);
        throw new NotImplementedException();
    }
}