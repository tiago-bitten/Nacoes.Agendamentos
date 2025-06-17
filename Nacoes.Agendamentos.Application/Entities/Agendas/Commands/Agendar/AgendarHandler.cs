using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Domain.Exceptions;
using Nacoes.Agendamentos.Domain.ValueObjects;
using Nacoes.Agendamentos.Infra.Extensions;
using AgendamentoId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agendamento>;
using AtividadeId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Atividade>;
using VoluntarioMinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.VoluntarioMinisterio>;

namespace Nacoes.agendamentos.application.entities.agendas.commands.agendar;

public sealed class AgendarHandler(IUnitOfWork uow,
                                   IValidator<AgendarCommand> agendarValidator,
                                   IAgendaRepository agendaRepository,
                                   IVoluntarioMinisterioRepository voluntarioMinisterioRepository,
                                   IAtividadeRepository atividadeRepository)
    : BaseHandler(uow), IAgendarHandler
{
    public async Task<Result<AgendamentoId, Error>> ExecutarAsync(AgendarCommand command, CancellationToken cancellation = default)
    {
        await agendarValidator.CheckAsync(command);

        var voluntarioMinisterioId = await voluntarioMinisterioRepository
            .GetOnlyIdAsync(command.VoluntarioMinisterioId)
            .OrElse(ExceptionFactory.VoluntarioNaoEncontrado);

        var atividadeId = await atividadeRepository
            .GetOnlyIdAsync(command.AtividadeId)
            .OrElse(ExceptionFactory.AtividadeNaoEncontrada);

        var agenda = await agendaRepository
            .GetOnlyIdAsync(command.AgendaId)
            .OrElse(ExceptionFactory.AgendaNaoEncontrada);  
            
        // TODO: criar strategy para agendamento manual, automatico...
        /*await Uow.BeginAsync();
        agenda.Agendar(voluntarioMinisterioId, atividadeId, EOrigemAgendamento.Manual);
        await Uow.CommitAsync(cancellation);

        return agenda.Agendamentos.Last().Id;*/

        throw new NotImplementedException();
    }
}