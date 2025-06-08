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
using Nacoes.Agendamentos.Infra.Extensions;
using AgendamentoId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agendamento>;
using AtividadeId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Atividade>;
using VoluntarioMinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.VoluntarioMinisterio>;
using AgendaId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agenda>;

namespace Nacoes.agendamentos.application.entities.agendas.commands.agendar;

public sealed class AgendarHandler(IUnitOfWork uow,
                                   IValidator<AgendarCommand> agendarValidator,
                                   IAgendaRepository agendaRepository,
                                   IVoluntarioRepository voluntarioRepository,
                                   IMinisterioRepository ministerioRepository)
    : BaseHandler(uow), IAgendarHandler
{
    public async Task<Result<AgendamentoId, Error>> ExcutarAsync(AgendarCommand command, AgendaId agendaId, CancellationToken cancellation = default)
    {
        await agendarValidator.CheckAsync(command);

        VoluntarioMinisterioId voluntarioMinisterioId = await voluntarioRepository.RecuperarPorVoluntarioMinisterio(command.VoluntarioMinisterioId)
                                                                                  .SelectMany(v => v.Ministerios)
                                                                                  .Select(vm => vm.Id)
                                                                                  .FirstOrDefaultAsync(cancellation)
                                                                                  .OrElse(ExceptionFactory.VoluntarioNaoEncontrado);

        AtividadeId atividadeId = await ministerioRepository.RecuperarPorAtividade(command.AtividadeId)
                                                            .SelectMany(m => m.Atividades)
                                                            .Select(a => a.Id)
                                                            .FirstOrDefaultAsync(cancellation)
                                                            .OrElse(ExceptionFactory.MinisterioNaoEncontrado);

        var agenda = await agendaRepository.GetByIdAsync(agendaId, includes: "Agendamentos")
                                           .OrElse(ExceptionFactory.AgendaNaoEncontrada);

        // TODO: criar strategy para agendamento manual, automatico...
        await Uow.BeginAsync();
        agenda.Agendar(voluntarioMinisterioId, atividadeId, EOrigemAgendamento.Manual);
        await Uow.CommitAsync(cancellation);

        return agenda.Agendamentos.Last().Id;
    }
}